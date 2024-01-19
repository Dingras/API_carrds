using API_carrds.Connections;
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
                    string query = "INSERT INTO " + TABLE + " (`username`, `password`, `name`, `last_name`, `email`) VALUES (@username,@password,@name,@last_name,@email)";
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn.Connect()))
                    {
                        cmd.Parameters.Add("@username", MySqlDbType.VarChar).Value = u.username;
                        cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = u.password;
                        cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = u.name;
                        cmd.Parameters.Add("@last_name", MySqlDbType.VarChar).Value = u.last_name;
                        cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = u.email;
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
            return "";
        }

        public User ListUser(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> ListUsers()
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
            return "";
        }
    }
}
