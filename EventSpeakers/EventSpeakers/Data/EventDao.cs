using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSpeakers.Models;
using System.Data.SqlClient;

namespace EventSpeakers.Data
{
    internal class EventDao
    {
        public int InsertEvent(Eventt eventt)
        {
            int insertedId = 0; 

            using (SqlConnection connection = new SqlConnection(ConnectionStrings.LOCAL))
            {
                connection.Open();
                string query = "insert into events(name,description,adress,startdate,startsat,endsat) output INSERTED.Id values (@name,@description,@adress,@startdate,@startsat,@endsat)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@name", eventt.Name);
                    cmd.Parameters.AddWithValue("@description", eventt.Description);
                    cmd.Parameters.AddWithValue("@adress", eventt.Adress);
                    cmd.Parameters.AddWithValue("@startdate", eventt.StartDate);
                    cmd.Parameters.AddWithValue("@startsat", eventt.StartsAt);
                    cmd.Parameters.AddWithValue("@endsat", eventt.EndsAt);

                    insertedId = (int)cmd.ExecuteScalar();
                }
            }

            return insertedId;
        }


        public Eventt GetEventById(int id)
        {
            Eventt eventt = null;
            using (SqlConnection connection = new SqlConnection(ConnectionStrings.LOCAL))
            {
                connection.Open();
                string query = "select TOP(1) * from Events where Id=@id";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.HasRows) return null;
                    while(reader.Read()) 
                    { 
                        eventt = new Eventt();
                        eventt.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        eventt.Name = reader.GetString(reader.GetOrdinal("Name"));
                        eventt.Description = reader.GetString(reader.GetOrdinal("Description"));
                        eventt.Adress = reader.GetString(reader.GetOrdinal("Adress"));
                        eventt.StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate"));
                        eventt.StartsAt = reader.GetString(reader.GetOrdinal("StartsAt"));
                        eventt.EndsAt = reader.GetString(reader.GetOrdinal("EndsAt"));
                    }
                }
                return eventt;
            }
        }

        public List<Eventt> GetEvents()
        {
            List<Eventt> eventts = new List<Eventt>();
            using (SqlConnection connection = new SqlConnection(ConnectionStrings.LOCAL))
            {
                connection.Open();
                string query = "Select Id,Name,description,startdate,startsat,endsat from Events";
                SqlCommand cmd = new SqlCommand(query, connection);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Eventt ent = new Eventt
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Adress = reader.GetString(3),
                            StartDate = reader.GetDateTime(4),
                            StartsAt = reader.GetString(5),
                            EndsAt = reader.GetString(6)
                        };
                        eventts.Add(ent);
                    }
                }
            }
            return eventts;
        }

        public int AddSpeaker(int eId, int sId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStrings.LOCAL))
            {
                connection.Open();
                string query = "Insert into EventSpeakers(eventid,speakerid) values (@EventId,@speakerId) ";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@EventId", eId);
                    cmd.Parameters.AddWithValue("@speakerId", sId);


                    return cmd.ExecuteNonQuery();
                }

            }
        }

        public int RemoveSpeaker(int eId, int sId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStrings.LOCAL))
            {
                connection.Open();
                string query = "Delete from EventSpeakers WHERE eventid = @eId AND speakerid = @sId ";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@eId", eId);
                    cmd.Parameters.AddWithValue("@sId", sId);

                    return cmd.ExecuteNonQuery();
                }

            }
        }

        public int DeleteEvent(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStrings.LOCAL))
            {
                connection.Open();
                string query = "Delete from events where id = @id";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery();
                }
            }

        }

    }
}




/*
public int DeleteSpeaker(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStrings.LOCAL))
            {
                connection.Open();
                string query = "Delete from speakers where id = @id";

                using(SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery();
                }
            }

        }
    */
