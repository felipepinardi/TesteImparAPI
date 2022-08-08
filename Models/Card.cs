using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Card
    {
        public Card()
        {
        }
        public int CardId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int PhotoId { get; set; }
        public string Base64 { get; set; }
        public static List<Card> GetAll()
        {
            var lista = new List<Card>();
            using (SqlConnection conn = new SqlConnection(Connection.Data))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT C.CardId, C.Name, C.[Status], C.PhotoId, P.Base64 FROM CARDS C LEFT JOIN PHOTO P ON C.PhotoId=P.PhotoId", conn);

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    lista.Add(new Card
                    {
                        CardId = Convert.ToInt32(rd["CardId"]),
                        Name = rd["Name"].ToString(),
                        Status = rd["Status"].ToString(),
                        PhotoId = rd["PhotoId"]==DBNull.Value ? 0 : Convert.ToInt32(rd["PhotoId"]),
                        Base64 = rd["Base64"].ToString()
                    });

                }
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public Card Create()
        {
            using (SqlConnection conn = new SqlConnection(Connection.Data))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("CreateCard @NAME,@STATUS, @BASE64", conn);
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar);
                cmd.Parameters["@NAME"].Value = this.Name;

                cmd.Parameters.Add("@STATUS", SqlDbType.VarChar);
                cmd.Parameters["@STATUS"].Value = this.Status;

                cmd.Parameters.Add("@BASE64", SqlDbType.VarChar);
                if (this.Base64 == "")
                {
                    cmd.Parameters["@BASE64"].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters["@BASE64"].Value = this.Base64;
                }

                this.CardId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return this;
        }

        public Card Update()
        {
            using (SqlConnection conn = new SqlConnection(Connection.Data))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("UpdateCard @NAME,@STATUS, @BASE64", conn);
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar);
                cmd.Parameters["@NAME"].Value = this.Name;

                cmd.Parameters.Add("@STATUS", SqlDbType.VarChar);
                cmd.Parameters["@STATUS"].Value = this.Status;

                cmd.Parameters.Add("@BASE64", SqlDbType.VarChar);
                if (this.Base64 == "")
                {
                    cmd.Parameters["@BASE64"].Value = this.Base64;
                }

                this.CardId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return this;
        }
        public static void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(Connection.Data))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM CARDS WHERE  CARDID=@ID", conn);
                cmd.Parameters.Add("@ID", SqlDbType.Int);
                cmd.Parameters["@ID"].Value = id;

                SqlDataReader rd = cmd.ExecuteReader();

                conn.Close();
                conn.Dispose();
            }
        }
    }
}
