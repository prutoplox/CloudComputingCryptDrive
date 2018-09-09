﻿using System;
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

        //DB Manager DB Methods
        public static bool RegisterUser(string username, string password, string email)
        {
            try
            {
                using (var connection = new SqlConnection(cb.ConnectionString))
                {
                    string registerUserSql = InsertIntoUserSQL(username, password, email);
                    SumbitSqlCommand(connection, registerUserSql);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return true;
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
                                        )";
            return createUserTable;
        }

        static string InsertIntoUserSQL(string username, string password, string email)
        {
            string returnstring = String.Format(@"INSERT INTO [dbo].[Users] (Username,Pw,Email) VALUES('{0}','{1}','{2}');", username, password, email);
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
    }
}