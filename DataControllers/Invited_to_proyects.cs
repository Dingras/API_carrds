using API_carrds.Connections;
using API_carrds.DataControllers.Interfaces;
using API_carrds.Models;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System.Text;

namespace API_carrds.DataControllers
{
    public class Invited_to_proyects : Icrud<InvitedToProyect>
    {
        private const string  TABLE = "invited_to_proyect ";
       
        public string Create(InvitedToProyect itp)
        {
            using (Connection cnn = new Connection())
            {
                string message = "Connection ERROR";

                try
                {
                    cnn.Open();
                    string query = "INSERT INTO " + TABLE + "(`id_proyect`, `id_user`,`status`) VALUES (@id_proyect, @id_user,@status)";
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn.Connect()))
                    {
                        cmd.Parameters.Add("@status", MySqlDbType.Int32).Value = itp.status;
                        cmd.Parameters.Add("@id_proyect", MySqlDbType.Int32).Value = itp.proyect.id;
                        cmd.Parameters.Add("@id_user", MySqlDbType.Int32).Value = itp.user.id;
                        

                        cmd.ExecuteNonQuery();
                    }
                    cnn.Close();
                    message = "OK";

                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    Console.WriteLine("StackTrace: " + ex.StackTrace);
                    cnn.Close();
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

        public IEnumerable<InvitedToProyect> GetAll()
        {
            List<InvitedToProyect> itpList = new List<InvitedToProyect>();
           
            using (Connection cnn = new Connection())
            {
                try
                {
                    cnn.Open();
                    string query = "SELECT `id`, `status`, `id_proyect`, `id_user` FROM " + TABLE;

                    using (MySqlCommand cmd = new MySqlCommand(query, cnn.Connect()))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                InvitedToProyect itp = new InvitedToProyect()
                                {
                                    id = reader.GetInt32(reader.GetOrdinal("id")),
                                    proyect = new Proyect( reader.GetInt32(reader.GetOrdinal("id_proyect"))),
                                    user = new User(reader.GetInt32(reader.GetOrdinal("id_user"))),
                                    status = reader.GetInt32(reader.GetOrdinal("status")),   
                                };

                                itpList.Add(itp);
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
                return itpList;
            }
        }

        public InvitedToProyect GetByID(int id)
        {
            using (Connection cnn = new Connection())
            {
                cnn.Open();
                string query = "SELECT * FROM " + TABLE + " WHERE `id`=@id";

                InvitedToProyect itp = null;

                using (MySqlCommand cmd = new MySqlCommand(query, cnn.Connect()))
                {
                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            itp = new InvitedToProyect
                            {
                                id = reader.GetInt32(reader.GetOrdinal("id")),
                                proyect = new Proyect(reader.GetInt32(reader.GetOrdinal("id_proyect"))),
                                user = new User(reader.GetInt32(reader.GetOrdinal("id_user"))),
                                status = reader.GetInt32(reader.GetOrdinal("status")),

                            };
                        }
                    }
                }

                return itp;
            }
        }

        public string Update(int id, InvitedToProyect itp)
        {
            using (Connection cnn = new Connection())
            {
                string message = "Connecction ERROR";

                try
                {
                    cnn.Open();

                    StringBuilder query = new StringBuilder("UPDATE " + TABLE + " SET ");
                    List<MySqlParameter> parameters = new List<MySqlParameter>();

                    if (itp.status >= 0)
                    {
                        query.Append("`status` = @status, ");
                        parameters.Add(new MySqlParameter("@status", MySqlDbType.VarChar) { Value = itp.status });
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
