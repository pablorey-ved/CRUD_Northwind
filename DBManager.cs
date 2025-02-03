using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindCRUD
{
    internal class DBManager
    {
        private static MySqlConnection con = null;
        private static readonly string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=root;database=northwind";

        public static MySqlConnection GetConnection()
        {
            if (con == null)
            {
                con = new MySqlConnection(connectionString);
            }

            if (con.State == System.Data.ConnectionState.Closed || con.State == System.Data.ConnectionState.Broken)
            {
                con.Open();
            }

            return con;
        }

        public void InsertarUser(string username, string password)
        {
            try
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand("INSERT INTO users (username, password) VALUES (@username, @password)", GetConnection()))
                {
                    mySqlCommand.Parameters.AddWithValue("@username", username);
                    mySqlCommand.Parameters.AddWithValue("@password", password);

                    mySqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar usuario: {ex.Message}");
            }
        }

        public void MostrarUsers()
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM users", GetConnection()))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["id"]}; Nombre de usuario: {reader["username"]}; Contraseña: {reader["password"]}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al mostrar usuarios: {ex.Message}");
            }
        }

        public static int GetUserByUsernameAndPassword(string username, string password)
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE username = @username AND password = @password", GetConnection()))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return Convert.ToInt32(reader["id"]);
                        }
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener usuario: {ex.Message}");
                return -1;
            }
        }

        public string GetUsernameById(int id)
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE id = @id", GetConnection()))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return Convert.ToString(reader["username"]);
                        }
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener usuario: {ex.Message}");
                return "";
            }
        }
    }
}
