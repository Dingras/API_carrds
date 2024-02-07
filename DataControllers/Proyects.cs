using API_carrds.Connections;
using API_carrds.DataControllers.Interfaces;
using API_carrds.Models;
using MySql.Data.MySqlClient;
using System.Text;

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
                        cmd.Parameters.Add("@created_by", MySqlDbType.Int32).Value = p.created_by.id;
                        cmd.Parameters.Add("@created_at", MySqlDbType.DateTime).Value = p.created_at;
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

        public Proyect GetByID(int id)
        {
            using (Connection cnn = new Connection())
            {
                cnn.Open();
                string query = "SELECT * FROM " + TABLE + " WHERE `id`=@id";
                Proyect proyect = null;
                using (MySqlCommand cmd = new MySqlCommand(query, cnn.Connect()))
                {
                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            proyect = new Proyect
                            {
                                id = reader.GetInt32(reader.GetOrdinal("id")),
                                name = reader.GetString(reader.GetOrdinal("name")),
                                description = reader.GetString(reader.GetOrdinal("description")),
                                created_by =new User (reader.GetInt32(reader.GetOrdinal("created_by"))),
                                created_at = reader.GetDateTime(reader.GetOrdinal("created_at")),
               
                            };
                        }
                    }
                }
                return proyect;
            }
        }

        public IEnumerable<Proyect> GetAll()
        {

            List<Proyect> proyects = new List<Proyect>();
           
            using (Connection cnn = new Connection())
            {
                try
                {
                    cnn.Open();
                    string query = "SELECT `id`,`name`,`description`,`created_by`,`created_at` FROM " + TABLE;

                    using (MySqlCommand cmd = new MySqlCommand(query, cnn.Connect()))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
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
                }catch (Exception ex) 
                {
                    Console.WriteLine($"Error de MySQL: {ex.Message}");
                    Console.WriteLine(ex.Message);
                }
            }
           
            return proyects;
        }

        public string Update(int id, Proyect p)
        {
            using (Connection cnn = new Connection())
            {
                string message = "Connection ERROR";
                try
                {
                    cnn.Open();
                    StringBuilder query = new StringBuilder("UPDATE " + TABLE + " SET ");
                    List<MySqlParameter> parameters = new List<MySqlParameter>();

                    if (!string.IsNullOrEmpty(p.name))
                    {
                        query.Append("`name` = @name, ");
                        parameters.Add(new MySqlParameter("@name", MySqlDbType.VarChar) { Value = p.name });
                    }
                    if (!string.IsNullOrEmpty(p.description))
                    {
                        query.Append("`description` = @description, ");
                        parameters.Add(new MySqlParameter("@description", MySqlDbType.VarChar) { Value = p.description });
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
    }
}
