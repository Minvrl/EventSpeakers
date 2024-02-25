using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSpeakers.Models;
using System.Data.SqlClient;

namespace EventSpeakers.Data
{
    internal class SpeakerDao
    {
        public int InsertSpeaker(Speaker speaker)
        {
            var result = 0;
            using (SqlConnection connection = new SqlConnection(ConnectionStrings.LOCAL))
            {
                connection.Open();
                string query = "insert into speakers(fullname,position,company,imageurl) values (@fullname,@position,@company,@imageurl)";
                using(SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@fullname", speaker.Fullname);
                    cmd.Parameters.AddWithValue("@position", speaker.Position);
                    cmd.Parameters.AddWithValue("@company", speaker.Company);
                    cmd.Parameters.AddWithValue("@imageurl", speaker.ImageURL);
                    result = cmd.ExecuteNonQuery(); ;

                }
            }
            return result;
        }

        public int UpdateSpeaker(Speaker speaker)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStrings.LOCAL))
            {
                connection.Open();
                string query = "Update speakers set fullname = @fullname,position = @position, company = @company where id = @id";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", speaker.Id);
                    cmd.Parameters.AddWithValue("@fullname", speaker.Fullname) ;
                    cmd.Parameters.AddWithValue("@position", speaker.Position);
                    cmd.Parameters.AddWithValue("@company", speaker.Company);

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public Speaker GetSpeakerById(int id)
        {
            Speaker speaker = null;
            using (SqlConnection connection = new SqlConnection(ConnectionStrings.LOCAL))
            {
                connection.Open();
                string query = "select TOP(1) * from Speakers where Id=@id";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.HasRows) return null;
                    while (reader.Read())
                    {
                        speaker = new Speaker();
                        speaker.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        speaker.Fullname = reader.GetString(reader.GetOrdinal("Fullname"));
                        speaker.Position = reader.GetString(reader.GetOrdinal("Position"));
                        speaker.Company = reader.GetString(reader.GetOrdinal("Company"));
                        speaker.ImageURL = reader.GetString(reader.GetOrdinal("ImageUrl"));


                    }
                }

            }
            return speaker;

        }

        public List<Speaker> GetSpeakers()
        {
            List<Speaker> speakers = new List<Speaker>();
            using(SqlConnection connection = new SqlConnection(ConnectionStrings.LOCAL))
            {
                connection.Open();
                string query = "Select Id,Fullname,position,company,imageurl from Speakers";
                SqlCommand cmd = new SqlCommand(query, connection);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Speaker spkr = new Speaker
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Fullname = reader.GetString(reader.GetOrdinal("Fullname")),
                            Position = reader.GetString(reader.GetOrdinal("Position")),
                            Company = reader.GetString(reader.GetOrdinal("Company")),
                            ImageURL = reader.GetString(reader.GetOrdinal("Imageurl"))
                        };
                        speakers.Add(spkr);

                    }
                }
            }
            return speakers;
        }

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


    }
}

