using API_carrds.Connections;
using API_carrds.DataControllers.Interfaces;
using API_carrds.Models;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1;
using System.Text;
using Task = API_carrds.Models.Task;


namespace API_carrds.DataControllers
{
    public class Tasks : Icrud<Task>
    {
        private const string TABLE = "tasks";
        public string Create(Task t)
        {

            using (Connection cnn = new Connection())
            {
                string message = "Connection ERROR";

                try
                {
                    cnn.Open();
                    string query = "INSERT INTO " + TABLE + "(`title`, `status`, `id_proyect`, `id_responsible`, `created_at`, `finalized_at`, `time_limit`, `info_text`, `id_springs`) VALUES (@title,@status,@id_proyect,@id_responsible,@created_at,@finalized_at,@time_limit,@info_text,@id_springs)";
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn.Connect()))
                    {
                        cmd.Parameters.Add("@title", MySqlDbType.VarChar).Value = t.title;
                        cmd.Parameters.Add("@status", MySqlDbType.Int32).Value = t.status;
                        cmd.Parameters.Add("@id_proyect", MySqlDbType.VarChar).Value = t.proyect;
                        cmd.Parameters.Add("@id_responsible", MySqlDbType.VarChar).Value = t.responsible;
                        cmd.Parameters.Add("@created_at", MySqlDbType.DateTime).Value = t.created_at;
                        cmd.Parameters.Add("@finalized_at", MySqlDbType.DateTime).Value = t.finalized_at;
                        cmd.Parameters.Add("@time_limit", MySqlDbType.DateTime).Value = t.time_limit;
                        cmd.Parameters.Add("@info_text", MySqlDbType.VarChar).Value = t.info_text;
                        cmd.Parameters.Add("@id_springs", MySqlDbType.VarChar).Value = t.spring;

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
                finally {
                    cnn.Close();
                }
                return message;
            }
        }

        public IEnumerable<Task> GetAll()
        {
            List<Task> tasksList = new List<Task>();
            using (Connection cnn = new Connection())
            {
                try
                {
                    cnn.Open();
                    string query = "SELECT `id`, `title`, `status`, `info_text`, `created_at`, `time_limit`, `finalized_at`, `id_proyect`, `id_responsible`, `id_springs` FROM " + TABLE;
                   
                    using (MySqlCommand cmd = new MySqlCommand(query,cnn.Connect()))
                    { 
                        using(MySqlDataReader reader =  cmd.ExecuteReader()) 
                        {
                            while (reader.Read())
                            {
                                Task task = new Task()
                                {
                                    id = reader.GetInt32(reader.GetOrdinal("id")),
                                    title = reader.GetString(reader.GetOrdinal("title")),
                                    status = reader.GetInt32(reader.GetOrdinal("status")),
                                    created_at = reader.GetDateTime(reader.GetOrdinal("created_at")),
                                    finalized_at = reader.GetDateTime(reader.GetOrdinal("finalized_at")),
                                    time_limit = reader.GetDateTime(reader.GetOrdinal("time_limit")),
                                    info_text = reader.GetString(reader.GetOrdinal("info_text")),
                                    proyect = new Proyect(reader.GetInt32(reader.GetOrdinal("id_proyect"))),
                                    spring = new Spring(reader.GetInt32(reader.GetOrdinal("id_springs"))),
                                    responsible = new User(reader.GetInt32(reader.GetOrdinal("id_responsible")))
                                };

                                tasksList.Add(task);
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
                return tasksList;
            }
        }

        public Task GetByID(int id)
        {
            using (Connection cnn = new Connection())
            {

                cnn.Open();
                string query = "SELECT * FROM " + TABLE + " WHERE `id`=@id";
                
                Task task = null;
                
                using (MySqlCommand cmd = new MySqlCommand(query, cnn.Connect()))
                {
                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            task = new Task
                            {
                                id = reader.GetInt32(reader.GetOrdinal("id")),
                                title = reader.GetString(reader.GetOrdinal("title")),
                                status = reader.GetInt32(reader.GetOrdinal("status")),
                                created_at = reader.GetDateTime(reader.GetOrdinal("created_at")),
                                finalized_at = reader.GetDateTime(reader.GetOrdinal("finalized_at")),
                                time_limit = reader.GetDateTime(reader.GetOrdinal("time_limit")),
                                info_text= reader.GetString(reader.GetOrdinal("info_text")),
                                proyect = new Proyect (reader.GetInt32(reader.GetOrdinal("id_proyect"))),
                                spring = new Spring(reader.GetInt32(reader.GetOrdinal("id_springs"))),
                                responsible = new User(reader.GetInt32(reader.GetOrdinal("id_responsible")))

                            };
                        }
                    }
                }
                
                return task;
            }

        }
    
        public string Update(int id, Task t)
        {
           
            using (Connection cnn = new Connection())
            {
                string message = "Connecction ERROR";

                try
                {
                    cnn.Open();
                   
                    StringBuilder query = new StringBuilder("UPDATE " + TABLE + " SET ");
                    List<MySqlParameter> parameters = new List<MySqlParameter>();

                    if (!string.IsNullOrEmpty(t.title))
                    {
                        query.Append("`title` = @title, ");
                        parameters.Add(new MySqlParameter("@title", MySqlDbType.VarChar) { Value = t.title });
                    }
                    if (!string.IsNullOrEmpty(t.info_text))
                    {
                        query.Append("`info_text` = @info_text, ");
                        parameters.Add(new MySqlParameter("@info_text", MySqlDbType.VarChar) { Value = t.info_text });
                    }
                    if (t.status >= 0)
                    {
                        query.Append("`status` = @status, ");
                        parameters.Add(new MySqlParameter("@status", MySqlDbType.VarChar) { Value = t.status });
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

