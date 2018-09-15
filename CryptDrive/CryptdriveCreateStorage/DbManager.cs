using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CryptdriveCloud
{
    class DbManager
    {
        private static SqlConnectionStringBuilder cb = CreateConnectionString();

        public static void CreateTableIfNotExists()
        {
            if (!doesTableExists())
            {
                CreateTableUser();
            }
        }

        static DbManager()
        {
        }

        //DB Manager System methods
        public static SqlConnectionStringBuilder CreateConnectionString()
        {
            var cb = new SqlConnectionStringBuilder();
            cb.DataSource = Cryptdrive.AzureLinkStringStorage.DB_DATASOURCE;
            cb.UserID = Cryptdrive.AzureLinkStringStorage.DB_USERID;
            cb.Password = Cryptdrive.AzureLinkStringStorage.DB_PASSWORD;
            cb.InitialCatalog = Cryptdrive.AzureLinkStringStorage.DB_INITALCATALOG;
            return cb;
        }

        public static int SumbitSqlCommand(SqlConnection connection, string sql)
        {
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                Console.WriteLine(rowsAffected + " = rows affected.");
                return rowsAffected;
            }
        }

        public static List<string> SubmitSelectUser(SqlConnection connection, string sql)
        {
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<string> resultList = new List<string>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        resultList.Add(reader.GetInt32(0).ToString());
                        resultList.Add(reader.GetString(1));
                        resultList.Add(reader.GetString(2));
                        resultList.Add(reader.GetString(3));
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
                connection.Close();
                Console.WriteLine("");
                return resultList;
            }
        }

        //DB Manager DB Methods
        public static bool RegisterUser(string username, string password, string email, string container)
        {
            try
            {
                using (var connection = new SqlConnection(cb.ConnectionString))
                {
                    string registerUserSql = InsertIntoUserSQL(username, password, email, container, 0);
                    SumbitSqlCommand(connection, registerUserSql);
                    return true;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public static List<string> GetUser(string username)
        {
            try
            {
                using (var connection = new SqlConnection(cb.ConnectionString))
                {
                    string getUserSQL = GetUserSQL(username);
                    return SubmitSelectUser(connection, getUserSQL);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                throw e;
            }
        }

        public static bool CreateTableUser()
        {
            try
            {
                using (var connection = new SqlConnection(cb.ConnectionString))
                {
                    string createTableSql = CreateUserDbSQL();
                    SumbitSqlCommand(connection, createTableSql);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return true;
        }

        public static bool DeleteUser(string username)
        {
            try
            {
                using (var connection = new SqlConnection(cb.ConnectionString))
                {
                    string deleteUserSQL = DeleteUserSQL(username);
                    SumbitSqlCommand(connection, deleteUserSQL);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return true;
        }

        public static bool UpdateUserPW(string username, string newPw)
        {
            try
            {
                using (var connection = new SqlConnection(cb.ConnectionString))
                {
                    string updateUserPwSQL = UpdateUserPasswordSQL(username, newPw);
                    SumbitSqlCommand(connection, updateUserPwSQL);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return true;
        }

        public static bool UpdateUserEmailConfirmed(string username)
        {
            try
            {
                using (var connection = new SqlConnection(cb.ConnectionString))
                {
                    string updateUserEmailConfirmedSQL = UpdateUserEmailConfirmedSQL(username);
                    SumbitSqlCommand(connection, updateUserEmailConfirmedSQL);
                    return true;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        // SQL String Methods. Return formatted SQL String

        public static string CreateUserDbSQL()
        {
            string createUserTable = @"CREATE TABLE Users
                                        (
                                        UserId   INT IDENTITY PRIMARY KEY,
                                        Username NVARCHAR(128) NOT NULL,
                                        Pw   NVARCHAR(128) NOT NULL,
                                        Email   NVARCHAR(128) NOT NULL,
                                        Container   NVARCHAR(512) NOT NULL,
                                        Confirmed INT Not NULL
                                        )";
            return createUserTable;
        }

        public static string InsertIntoUserSQL(string username, string password, string email, string container, int confirmed)
        {
            string returnstring = String.Format(@"INSERT INTO [dbo].[Users] (Username,Pw,Email,Container,Confirmed) VALUES('{0}','{1}','{2}','{3}','{4}');", username, password, email, container, confirmed);
            return returnstring;
        }

        public static string DeleteUserSQL(string username)
        {
            string returnstring = String.Format(@"DELETE FROM [dbo].[Users] WHERE Username='{0}'", username);
            return returnstring;
        }

        public static string UpdateUserPasswordSQL(string username, string newPassword)
        {
            string returnstring = String.Format(@"UPDATE [dbo].[Users] SET [dbo].[Users].[Pw] = '{0}' WHERE [dbo].[Users].[Username] = '{1}'", newPassword, username);
            return returnstring;
        }

        public static string UpdateUserEmailSQL(string username, string newEmail)
        {
            string returnstring = String.Format(@"UPDATE [dbo].[Users] SET [dbo].[Users].[Email] = '{0}' WHERE [dbo].[Users].[Username] = '{1}'", newEmail, username);
            return returnstring;
        }

        public static string UpdateUserEmailConfirmedSQL(string username)
        {
            string returnstring = String.Format(@"UPDATE [dbo].[Users] SET [dbo].[Users].[Confirmed] = {0} WHERE [dbo].[Users].[Username] = '{1}'", 1, username);
            return returnstring;
        }

        public static string DropUserTable()
        {
            string returnstring = String.Format(@"DROP TABLE [dbo].[Users];");
            return returnstring;
        }

        public static string GetUserSQL(string username)
        {
            string returnstring = String.Format(@"SELECT * FROM [dbo].[Users] WHERE Username='{0}'", username);
            return returnstring;
        }

        public static bool doesTableExists()
        {
            try
            {
                using (var connection = new SqlConnection(cb.ConnectionString))
                {
                    string checkIfTableExistsString = "SELECT * FROM sys.tables t JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE s.name = 'dbo' AND t.name = 'Users'";
                    int rows = SumbitSqlCommand(connection, checkIfTableExistsString);
                    if (rows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
    }
}
