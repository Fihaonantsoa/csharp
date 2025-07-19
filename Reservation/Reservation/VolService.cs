using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation
{
    public class VolService
    {
        public void Ajouter(Vol p)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("INSERT INTO vol (idvol, datedep, datearr, statut) VALUES (@id, @datedep, @datearr, @statut)", conn);
                cmd.Parameters.AddWithValue("@id", p.Id);
                cmd.Parameters.AddWithValue("@datedep", p.Datedep);
                cmd.Parameters.AddWithValue("@datearr", p.Datearr);
                cmd.Parameters.AddWithValue("@statut", p.Statut);
                 cmd.ExecuteNonQuery();
            }
        }

        public void Modifier(Vol p)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("UPDATE vol SET datedep=@datedep, datearr=@datearr, statut=@statut WHERE idvol=@id", conn);
                cmd.Parameters.AddWithValue("@id", p.Id);
                cmd.Parameters.AddWithValue("@datedep", p.Datedep);
                cmd.Parameters.AddWithValue("@datearr", p.Datearr);
                cmd.Parameters.AddWithValue("@statut", p.Statut);
                cmd.ExecuteNonQuery();
            }
        }

        public void Supprimer(string id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("DELETE FROM vol WHERE idvol=@id", conn);
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
                var cmd = new MySqlCommand("SELECT * FROM vol", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }
    }



}
