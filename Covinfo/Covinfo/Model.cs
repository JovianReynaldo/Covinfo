using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Covinfo
{
    class Model
    {
        public SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        public Model()
        {
            //konfigurasi konektivitas database
            builder.DataSource = "localhost";
            builder.UserID = "root";
            builder.Password = "123";
            builder.InitialCatalog = "Covinfo";
        }

        public Boolean Login(string username, string password)
        {
            using SqlConnection connection = new SqlConnection(builder.ConnectionString);
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = " SELECT * FROM dbo.admin" +
                                "    WHERE username = '" + username + "'" +
                                "      AND PWDCOMPARE('" + password + "', password) = 1; ";
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            Boolean result = reader.HasRows;

            connection.Close();
            return result;

        }

        public FAQ Bot(string keyword)
        {
            using SqlConnection connection = new SqlConnection(builder.ConnectionString);
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT TOP 1 * " +
                                "    FROM dbo.faq as faq," +
                                "         STRING_SPLIT(REPLACE('" + keyword + "', ' ', ';'), ';') data" +
                                "   WHERE faq.keyword LIKE('%' + data.value + '%') AND flag = 1";
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            FAQ result = null;

            if (reader.HasRows)
            {
                using (reader)
                {
                    while (reader.Read())
                    {
                        result = new FAQ(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                    }
                }
            }

            connection.Close();
            return result;
        }

        public List<FAQ> GetAllFAQ()
        {
            using SqlConnection connection = new SqlConnection(builder.ConnectionString);
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM dbo.faq WHERE flag != 0";
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<FAQ> list = new List<FAQ>();

            if (reader.HasRows)
            {
                using (reader)
                {
                    while (reader.Read())
                    {
                        list.Add(new FAQ(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                    }
                }
            }

            connection.Close();
            return list;
        }

        public FAQ GetFAQbyId(int id)
        {
            using SqlConnection connection = new SqlConnection(builder.ConnectionString);
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT TOP 1 * FROM dbo.faq WHERE id = '" + id + "' AND  flag != 0";
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                using (reader)
                {
                    while (reader.Read())
                    {
                        return new FAQ(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                    }
                }
            }
            else
            {
                Console.WriteLine("No information found");
            }
            connection.Close();
            return null;
        }
        public void CreateFAQ(string keyword, string head, string body)
        {
            using SqlConnection connection = new SqlConnection(builder.ConnectionString);
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO dbo.faq(keyword, head, body, flag) " +
                                  "VALUES(@param1,@param2,@param3,1)";
            command.Parameters.AddWithValue("@param1", keyword);
            command.Parameters.AddWithValue("@param2", head);
            command.Parameters.AddWithValue("@param3", body);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }
        public void EditFAQ(string keyword, string head, string body, int id)
        {
            using SqlConnection connection = new SqlConnection(builder.ConnectionString);
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE dbo.faq SET keyword = @param1" +
                                  " ,head = @param2 " +
                                  " ,body = @param3 " +
                                  "WHERE id = @param4";
            command.Parameters.AddWithValue("@param1", keyword);
            command.Parameters.AddWithValue("@param2", head);
            command.Parameters.AddWithValue("@param3", body);
            command.Parameters.AddWithValue("@param4", id);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }
        public void DisableFAQ(int id)
        {
            using SqlConnection connection = new SqlConnection(builder.ConnectionString);
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE dbo.faq SET flag = 0" +
                                  " WHERE id = @param1";
            command.Parameters.AddWithValue("@param1", id);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }
    }
}
