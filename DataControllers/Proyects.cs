using API_carrds.Connections;
using API_carrds.DataControllers.Interfaces;
using API_carrds.Models;
using MySql.Data.MySqlClient;
using System.Dynamic;

namespace API_carrds.DataControllers
{
    public class Proyects : Icrud<Proyect>
    {
        private const string TABLE = "proyects";
        public string Create(Proyect p)
        {
            using (Connection cnn = new Connection())
            {
                string message = "Conection ERROR";

                try
                {
                    cnn.Open();
                    string query = "INSERT INTO " + TABLE + "(`name`, `description`,`created_by`,`created_at`) VALUES (@name,@description,@created_by,@created_at)";
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn.Connect()))
                    {
                        cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = p.name;
                        cmd.Parameters.Add("@description", MySqlDbType.VarChar).Value = p.description;
                        cmd.Parameters.Add("@created_by", MySqlDbType.VarChar).Value = p.created_by;
                        cmd.Parameters.Add("@created_at", MySqlDbType.VarChar).Value = p.created_at;
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
                    string query = "DELETE FROM proyects WHERE id=@id";
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

        public Proyect ListUser(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Proyect> ListUsers()
        {
            List<Proyect> proyects = new List<Proyect>();
            using (Connection cnn = new Connection())
            {
                cnn.Open();
                string query = "SELECT `id`,`name`,`description`,`created_by`,`created_at` FROM " + TABLE;

                using (MySqlCommand cmd = new MySqlCommand(query, cnn.Connect()))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    { 
                        while(reader.Read())
                        {
                            Proyect proyect = new Proyect
                            {
                                id = reader.GetInt32(reader.GetOrdinal("id")),
                                name = reader.GetString(reader.GetOrdinal("name")),
                                description = reader.GetString(reader.GetOrdinal("description")),
                                created_by = new User(reader.GetInt32(reader.GetOrdinal("created_by"))),
                                created_at = reader.GetDateTime(reader.GetOrdinal("created_at"))
                            };

                            proyects.Add(proyect);
                        }
                    }
                }
            }

            return proyects;
        }

        public Proyect GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public string Update(int id, Proyect t)
        {
            using (Connection cnn = new Connection())
            {
                string message = "Connection ERROR";
                try
                {
                    cnn.Open();
                    string query = "UPDATE " + TABLE + " SET name = @name, description = @description, created_at = @created_at WHERE id=@id";
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn.Connect()))
                    {
                        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                        cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = t.name;
                        cmd.Parameters.Add("@description", MySqlDbType.VarChar).Value = t.description;
                        cmd.Parameters.Add("created_at", MySqlDbType.DateTime).Value = t.created_at;

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
