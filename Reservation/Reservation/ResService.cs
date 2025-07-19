using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation
{
    public class ResService
    {
        public void Ajouter(Reservation p)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("INSERT INTO reservation (idreserve, idvoyageur, datereserve, prix, modepaie) VALUES (@id, @idv, @dateres, @prix, @mode)", conn);
                cmd.Parameters.AddWithValue("@id", p.Id);
                cmd.Parameters.AddWithValue("@idv", p.Idvoyageur);
                cmd.Parameters.AddWithValue("@dateres", p.Dateres);
                cmd.Parameters.AddWithValue("@prix", p.Prix);
                cmd.Parameters.AddWithValue("@mode", p.ModePaie);
                cmd.ExecuteNonQuery();
            }
        }

        public void Modifier(Reservation p)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("UPDATE reservation SET idvoyageur=@idv, datereserve=@dateres, prix=@prix, modepaie=@mode WHERE idreserve=@id", conn);
                cmd.Parameters.AddWithValue("@id", p.Id);
                cmd.Parameters.AddWithValue("@idv", p.Idvoyageur);
                cmd.Parameters.AddWithValue("@dateres", p.Dateres);
                cmd.Parameters.AddWithValue("@prix", p.Prix);
                cmd.Parameters.AddWithValue("@mode", p.ModePaie);
                cmd.ExecuteNonQuery();
            }
        }

        public void Supprimer(string id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("DELETE FROM reservation WHERE idreserve=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable ObtenirTous()
        {
            DataTable dt = new DataTable();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM reservation", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }

    }


}
