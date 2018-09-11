﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CryptdriveCloud
{
    static class DbManager
    {
        private static SqlConnectionStringBuilder cb = CreateConnectionString();

        //DB Manager System methods
        private static SqlConnectionStringBuilder CreateConnectionString()
        {
            var cb = new SqlConnectionStringBuilder();
            cb.DataSource = Cryptdrive.AzureLinkStringStorage.DB_DATASOURCE;
            cb.UserID = Cryptdrive.AzureLinkStringStorage.DB_USERID;
            cb.Password = Cryptdrive.AzureLinkStringStorage.DB_PASSWORD;
            cb.InitialCatalog = Cryptdrive.AzureLinkStringStorage.DB_INITALCATALOG;
            return cb;
        }

        static void SumbitSqlCommand(SqlConnection connection, string sql)
        {
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                Console.WriteLine(rowsAffected + " = rows affected.");
            }
        }

        static List<string> SubmitSelectUser(SqlConnection connection, string sql)
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
                    string registerUserSql = InsertIntoUserSQL(username, password, email, container);
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

        // SQL String Methods. Return formatted SQL String

        static string CreateUserDbSQL()
        {
            string createUserTable = @"CREATE TABLE Users
                                        (
                                        UserId   INT IDENTITY PRIMARY KEY,
                                        Username NVARCHAR(128) NOT NULL,
                                        Pw   NVARCHAR(128) NOT NULL,
                                        Email   NVARCHAR(128) NOT NULL,
                                        Container   NVARCHAR(512) NOT NULL,
                                        )";
            return createUserTable;
        }

        static string InsertIntoUserSQL(string username, string password, string email, string container)
        {
            string returnstring = String.Format(@"INSERT INTO [dbo].[Users] (Username,Pw,Email,Container) VALUES('{0}','{1}','{2}','{3}');", username, password, email, container);
            return returnstring;
        }

        static string DeleteUserSQL(string username)
        {
            string returnstring = String.Format(@"DELETE FROM [dbo].[Users] WHERE Username='{0}'", username);
            return returnstring;
        }

        static string UpdateUserPasswordSQL(string username, string newPassword)
        {
            string returnstring = String.Format(@"UPDATE [dbo].[Users] SET [dbo].[Users].[Pw] = '{0}' WHERE [dbo].[Users].[Username] = '{1}'", newPassword, username);
            return returnstring;
        }

        static string UpdateUserEmailSQL(string username, string newEmail)
        {
            string returnstring = String.Format(@"UPDATE [dbo].[Users] SET [dbo].[Users].[Email] = '{0}' WHERE [dbo].[Users].[Username] = '{1}'", newEmail, username);
            return returnstring;
        }

        static string DropUserTable()
        {
            string returnstring = String.Format(@"DROP TABLE [dbo].[Users];");
            return returnstring;
        }

        static string GetUserSQL(string username)
        {
            string returnstring = String.Format(@"SELECT * FROM [dbo].[Users] WHERE Username='{0}'", username);
            return returnstring;
        }
    }
}
