﻿using API_carrds.Connections;
using API_carrds.DataControllers.Interfaces;
using API_carrds.Models;
using MySql.Data.MySqlClient;
using System.Data;

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
                    string query = "UPDATE "+ TABLE +" SET `username`=@username,`password`=@password,`name`=@name,`last_name`=@last_name,`email`=@email,`avatar_url`=@avatar_url WHERE `id`=@id";
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn.Connect()))
                    {
                        cmd.Parameters.Add("@username", MySqlDbType.Int32).Value = id;
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

    }
}
