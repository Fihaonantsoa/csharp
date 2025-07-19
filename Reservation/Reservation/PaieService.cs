using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation
{
    public class PaieService
    {
        public void Ajouter(Paiement p)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("INSERT INTO paiement (idpaie, idreservation, montant, datepaie) VALUES (@id, @idres, @montant, @date)", conn);
                cmd.Parameters.AddWithValue("@id", p.Id);
                cmd.Parameters.AddWithValue("@idres", p.IDres);
                cmd.Parameters.AddWithValue("@montant", p.Montant);
                cmd.Parameters.AddWithValue("@date", p.datePaie);
                cmd.ExecuteNonQuery();
            }
        }

        public void Modifier(Paiement p)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("UPDATE paiement SET idreservation=@idres, montant=@montant, datepaie=@date WHERE idpaie=@id", conn);
                cmd.Parameters.AddWithValue("@id", p.Id);
                cmd.Parameters.AddWithValue("@idres", p.IDres);
                cmd.Parameters.AddWithValue("@montant", p.Montant);
                cmd.Parameters.AddWithValue("@date", p.datePaie);
                cmd.ExecuteNonQuery();
            }
        }

        public void Supprimer(string id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("DELETE FROM paiement WHERE idpaie=@id", conn);
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
                var cmd = new MySqlCommand("SELECT * FROM paiement", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }
    }



}
