using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation
{
    public class BilletService
    {
        public void Ajouter(Billet p)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("INSERT INTO billet (idbillet, idvoyageur, idvol, idreservation, classe, siege, prix, etat) VALUES (@id, @idv, @idvol, @idres, @classe, @siege, @prix, @etat)", conn);
                cmd.Parameters.AddWithValue("@id", p.Id);
                cmd.Parameters.AddWithValue("@idv", p.Passager);
                cmd.Parameters.AddWithValue("@idvol", p.Vol);
                cmd.Parameters.AddWithValue("@idres", p.Res);
                cmd.Parameters.AddWithValue("@classe", p.Classe);
                cmd.Parameters.AddWithValue("@siege", p.Siege);
                cmd.Parameters.AddWithValue("@prix", p.Prix);
                cmd.Parameters.AddWithValue("@etat", p.Etat);
                cmd.ExecuteNonQuery();
            }
        }

        public void Modifier(Billet p)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("UPDATE billet SET idvoyageur=@idv, idvol=@idvol, idreservation=@idres, classe=@classe, siege=@siege, prix=@prix, etat=@etat WHERE idbillet=@id", conn);
                cmd.Parameters.AddWithValue("@id", p.Id);
                cmd.Parameters.AddWithValue("@idv", p.Passager);
                cmd.Parameters.AddWithValue("@idvol", p.Vol);
                cmd.Parameters.AddWithValue("@idres", p.Res);
                cmd.Parameters.AddWithValue("@classe", p.Classe);
                cmd.Parameters.AddWithValue("@siege", p.Siege);
                cmd.Parameters.AddWithValue("@prix", p.Prix);
                cmd.Parameters.AddWithValue("@etat", p.Etat);
                cmd.ExecuteNonQuery();
            }
        }

        public void Supprimer(string id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("DELETE FROM billet WHERE idbillet=@id", conn);
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
                var cmd = new MySqlCommand("SELECT * FROM billet", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }
        public DataTable ObtenirDetaille()
        {
            DataTable dt = new DataTable();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT billet.*, P.nom FROM billet JOIN passager P ON billet.idvoyageur=P.id", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }
    }



}
