using API_carrds.Connections;
using API_carrds.DataControllers.Interfaces;
using API_carrds.Models;
using Microsoft.AspNetCore.Http.Extensions;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

namespace API_carrds.DataControllers
{
    public class Users : Icrud<User>
    {
        private const string TABLE = "users";
        public string Create(User u)
        {
            using (Connection cnn = new Connection())
            {
                string message = "Connection ERROR";
                try
                {
                    cnn.Open();
                    string query = "INSERT INTO " + TABLE + " (`username`, `password`, `name`, `last_name`, `email`,`avatar_url`) VALUES (@username,@password,@name,@last_name,@email,@avatar_url)";
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn.Connect()))
                    {
                        cmd.Parameters.Add("@username", MySqlDbType.VarChar).Value = u.username;
                        cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = u.password;
                        cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = u.name;
                        cmd.Parameters.Add("@last_name", MySqlDbType.VarChar).Value = u.last_name;
                        cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = u.email;
                        cmd.Parameters.Add("@avatar_url", MySqlDbType.VarChar).Value = u.avatar_url;
                        cmd.ExecuteNonQuery();

                    }
                    cnn.Close();
                    message = "OK";
                }
                catch (Exception ex)
                {
                    cnn.Close();
                    message = ex.Message;
                }
                finally 
                {
                    cnn.Close();
                }

                return message;

            }
        }

        public string Delete(int id)
        {
            using (Connection cnn = new Connection())
            {
                string message = "Connection ERROR";
                try
                {
                    cnn.Open();
                    string query = "DELETE FROM "+ TABLE +" WHERE `users`.`id` = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn.Connect()))
                    {
                        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                        cmd.ExecuteNonQuery();
                    }
                    cnn.Close();
                    message = "OK";
                }
                catch (Exception ex)
                {
                    cnn.Close();
                    message = ex.Message;
                }
                finally
                {
                    cnn.Close();
                }

                return message;
            }
        }

        public User GetByID(int id)
        {
            using (Connection cnn = new Connection())
            {
                cnn.Open();
                string query = "SELECT * FROM "+ TABLE +" WHERE `id`=@id";
                User user = null;
                using (MySqlCommand cmd = new MySqlCommand(query, cnn.Connect()))
                {
                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                id = reader.GetInt32(reader.GetOrdinal("id")),
                                username = reader.GetString(reader.GetOrdinal("username")),
                                password = reader.GetString(reader.GetOrdinal("password")),
                                name = reader.GetString(reader.GetOrdinal("name")),
                                last_name = reader.GetString(reader.GetOrdinal("last_name")),
                                email = reader.GetString(reader.GetOrdinal("email"))
                            };
                        }
                    }
                }
                return user;
            }
            
        }

        public IEnumerable<User> GetAll()
        {
            List<User> users = new List<User>();
            using (Connection cnn = new Connection())
            {
                cnn.Open();
                string query = "SELECT `id`, `username`, `password`, `name`, `last_name`, `email` FROM " + TABLE;

                using (MySqlCommand cmd = new MySqlCommand(query, cnn.Connect()))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User
                            {
                                id = reader.GetInt32(reader.GetOrdinal("id")),
                                username = reader.GetString(reader.GetOrdinal("username")),
                                password = reader.GetString(reader.GetOrdinal("password")),
                                name = reader.GetString(reader.GetOrdinal("name")),
                                last_name = reader.GetString(reader.GetOrdinal("last_name")),
                                email = reader.GetString(reader.GetOrdinal("email"))
                            };
                            users.Add(user);
                        }
                    }
                }
            }
            return users;
        }

        public string Update(int id, User u)
        {
            using (Connection cnn = new Connection())
            {
                string message = "Connection ERROR";
                try
                {
                    cnn.Open();
                    
                    StringBuilder query = new StringBuilder("UPDATE " + TABLE +" SET ");
                    List<MySqlParameter> parameters = new List<MySqlParameter>();
                    
                    if (!string.IsNullOrEmpty(u.username))
                    {
                        query.Append("`username` = @username, ");
                        parameters.Add(new MySqlParameter("@username", MySqlDbType.VarChar) { Value = u.username });
                    }
                    if (!string.IsNullOrEmpty(u.password))
                    {
                        query.Append("`password` = @password, ");
                        parameters.Add(new MySqlParameter("@password", MySqlDbType.VarChar) { Value = u.password });
                    }
                    if (!string.IsNullOrEmpty(u.name))
                    {
                        query.Append("`name` = @name, ");
                        parameters.Add(new MySqlParameter("@name", MySqlDbType.VarChar) { Value = u.name });
                    }
                    if (!string.IsNullOrEmpty(u.last_name))
                    {
                        query.Append("`last_name` = @last_name, ");
                        parameters.Add(new MySqlParameter("@last_name", MySqlDbType.VarChar) { Value = u.last_name });
                    }
                    if (!string.IsNullOrEmpty(u.email))
                    {
                        query.Append("`email` = @email, ");
                        parameters.Add(new MySqlParameter("@email", MySqlDbType.VarChar) { Value = u.email });
                    }
                    if (!string.IsNullOrEmpty(u.avatar_url))
                    {
                        query.Append("`avatar_url` = @avatar_url, ");
                        parameters.Add(new MySqlParameter("@avatar_url", MySqlDbType.VarChar) { Value = u.avatar_url });
                    }
                    query.Remove(query.Length - 2, 2); // Borra el espacio y la coma del final de la consulta
                    
                    query.Append(" WHERE `id` = @id"); // Agrego la condicion al final de la consulta
                    parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32) { Value = id });
                    
                    using (MySqlCommand cmd = new MySqlCommand(query.ToString(), cnn.Connect()))
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                        cmd.ExecuteNonQuery();

                    }
                    cnn.Close();
                    message = "OK";
                }
                catch (Exception ex)
                {
                    cnn.Close();
                    message = ex.Message;
                }
                finally
                {
                    cnn.Close();
                }
                return message;
            }
        }

        public User? AuthUser(User u)
        {
            using (Connection cnn = new Connection())
            {
                cnn.Open();
                string query = "SELECT * FROM " + TABLE + " WHERE `username` = @username AND `password` = @password";
                User user = null;
                using (MySqlCommand cmd = new MySqlCommand(query, cnn.Connect()))
                {
                    cmd.Parameters.Add("@username", MySqlDbType.VarChar).Value = u.username;
                    cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = u.password;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                id = reader.GetInt32(reader.GetOrdinal("id")),
                                username = reader.GetString(reader.GetOrdinal("username")),
                                password = reader.GetString(reader.GetOrdinal("password")),
                                name = reader.GetString(reader.GetOrdinal("name")),
                                last_name = reader.GetString(reader.GetOrdinal("last_name")),
                                email = reader.GetString(reader.GetOrdinal("email"))
                            };
                        }
                    }
                }
                return user;
            }
        }

    }
}
