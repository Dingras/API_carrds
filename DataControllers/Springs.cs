using API_carrds.Connections;
using API_carrds.DataControllers.Interfaces;
using API_carrds.Models;
using MySql.Data.MySqlClient;
using System.Text;

namespace API_carrds.DataControllers
{
    public class Springs:Icrud<Spring>
    {
        private const string TABLE = "springs";

        public string Create(Spring s)
        {
            using (Connection cnn = new Connection())
            {
                string message = "Connection ERROR";
                try
                {
                    cnn.Open();
                    string query = "INSERT INTO " + TABLE + " (`title`, `description`, `id_proyect`) VALUES (@title,@description,@id_proyect)";
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn.Connect()))
                    {
                        cmd.Parameters.Add("@title", MySqlDbType.VarChar).Value = s.title;
                        cmd.Parameters.Add("@description", MySqlDbType.VarChar).Value = s.description;
                        cmd.Parameters.Add("@id_proyect", MySqlDbType.Int32).Value = s.proyect.id;
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
            string message = "Conecction ERROR";

            using (Connection cnn = new Connection())
            {
                try
                {
                    cnn.Open();
                    string query = "DELETE FROM " + TABLE + " WHERE id=@id";
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

        public IEnumerable<Spring> GetAll()
        {
            List<Spring> springList = new List<Spring>();
            using (Connection cnn = new Connection())
            {
                try
                {
                    cnn.Open();
                    string query = "SELECT `id`, `title`, `description`, `id_proyect` FROM " + TABLE;

                    using (MySqlCommand cmd = new MySqlCommand(query, cnn.Connect()))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Spring spring = new Spring()
                                {
                                    id = reader.GetInt32(reader.GetOrdinal("id")),
                                    title = reader.GetString(reader.GetOrdinal("title")),
                                    description = reader.GetString(reader.GetOrdinal("description")),
                                    proyect = new Proyect(reader.GetInt32(reader.GetOrdinal("id_proyect")))
                                };

                                springList.Add(spring);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    cnn.Close();
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    cnn.Close();
                }
                return springList;
            }
        }

        public IEnumerable<Spring> GetByProyect(int id_proyect)
        {
            List<Spring> springList = new List<Spring>();
            using (Connection cnn = new Connection())
            {
                try
                {
                    cnn.Open();
                    string query = "SELECT `id`, `title`, `description`, `id_proyect` FROM " + TABLE + " WHERE `id_proyect` = @id_proyect";

                    using (MySqlCommand cmd = new MySqlCommand(query, cnn.Connect()))
                    {
                        cmd.Parameters.Add("@id_proyect", MySqlDbType.Int32).Value = id_proyect;
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Spring spring = new Spring()
                                {
                                    id = reader.GetInt32(reader.GetOrdinal("id")),
                                    title = reader.GetString(reader.GetOrdinal("title")),
                                    description = reader.GetString(reader.GetOrdinal("description")),
                                    proyect = new Proyect(reader.GetInt32(reader.GetOrdinal("id_proyect")))
                                };

                                springList.Add(spring);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    cnn.Close();
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    cnn.Close();
                }
                return springList;
            }
        }

        public Spring GetByID(int id)
        {
            using (Connection cnn = new Connection())
            {

                cnn.Open();
                string query = "SELECT * FROM " + TABLE + " WHERE `id`=@id";

                Spring spring = null;

                using (MySqlCommand cmd = new MySqlCommand(query, cnn.Connect()))
                {
                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            spring = new Spring
                            {
                                id = reader.GetInt32(reader.GetOrdinal("id")),
                                title = reader.GetString(reader.GetOrdinal("title")),
                                description = reader.GetString(reader.GetOrdinal("description")),
                                proyect = new Proyect(reader.GetInt32(reader.GetOrdinal("id_proyect")))
                            };
                        }
                    }
                }
                return spring;
            }
        }

        public string Update(int id, Spring s)
        {
            using (Connection cnn = new Connection())
            {
                string message = "Connecction ERROR";

                try
                {
                    cnn.Open();

                    StringBuilder query = new StringBuilder("UPDATE " + TABLE + " SET ");
                    List<MySqlParameter> parameters = new List<MySqlParameter>();

                    if (!string.IsNullOrEmpty(s.title))
                    {
                        query.Append("`title` = @title, ");
                        parameters.Add(new MySqlParameter("@title", MySqlDbType.VarChar) { Value = s.title });
                    }
                    if (!string.IsNullOrEmpty(s.description))
                    {
                        query.Append("`description` = @description, ");
                        parameters.Add(new MySqlParameter("@description", MySqlDbType.VarChar) { Value = s.description });
                    }
                    query.Remove(query.Length - 2, 2);

                    query.Append(" WHERE `id` = @id");
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
