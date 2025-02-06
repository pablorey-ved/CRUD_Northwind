using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

        public static void CloseConnection()
        {
            MySqlConnection con = GetConnection();
            con.Close();
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

        public static DataTable GetProducts()
        {
            try
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT products.ProductName, categories.CategoryName FROM products JOIN categories ON products.CategoryID = categories.CategoryID", GetConnection()))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los productos: {ex.Message}");
                return null;
            }
        }

        public static DataTable GetCategories()
        {
            try
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT CategoryID, CategoryName FROM categories", GetConnection()))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las categorías: {ex.Message}");
                return null;
            }
        }

        public static int InsertProduct(string productName, int categoryID)
        {
            bool yaExiste = ProductExists(productName);

            if (yaExiste) return -1;

            try
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand("INSERT INTO products (ProductName, CategoryID) VALUES (@product, @category)", GetConnection()))
                {
                    mySqlCommand.Parameters.AddWithValue("@product", productName);
                    mySqlCommand.Parameters.AddWithValue("@category", categoryID);

                    return mySqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar producto: {ex.Message}");
                return -1;
            }
        }

        public static int GetCategoryIDByProductName(string productName)
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand("SELECT CategoryID FROM products WHERE ProductName = @product", GetConnection()))
                {
                    command.Parameters.AddWithValue("@product", productName);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return Convert.ToInt32(reader["CategoryID"]);
                        }
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener categoría: {ex.Message}");
                return -1;
            }
        }

        public static bool ProductExists(string productName)
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM products WHERE ProductName = @product", GetConnection()))
                {
                    command.Parameters.AddWithValue("@product", productName);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener categoría: {ex.Message}");
                return true;
            }
        }

        public static int UpdateProduct(string newProductName, string oldProductName, int categoryID) {
            try
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand("UPDATE products SET CategoryID=@cat, ProductName=@product WHERE ProductName=@oldProduct", GetConnection()))
                {
                    mySqlCommand.Parameters.AddWithValue("@product", newProductName);
                    mySqlCommand.Parameters.AddWithValue("@oldProduct", oldProductName);
                    mySqlCommand.Parameters.AddWithValue("@cat", categoryID);

                    return mySqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar producto: {ex.Message}");
                return -1;
            }
        }

        public static int DeleteProduct(string productName)
        {
            try
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand("DELETE FROM products WHERE ProductName=@product", GetConnection()))
                {
                    mySqlCommand.Parameters.AddWithValue("@product", productName);

                    return mySqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar producto: {ex.Message}");
                return -1;
            }
        }
    }
}
