using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation
{
    public class PassagerService
    {
        public void Ajouter(Passager p)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("INSERT INTO passager (id, nom, passeport, nationalite, telephone, idreserve) VALUES (@id, @nom, @passeport, @nationalite, @telephone, @idres)", conn);
                cmd.Parameters.AddWithValue("@id", p.Id);
                cmd.Parameters.AddWithValue("@nom", p.Nom);
                cmd.Parameters.AddWithValue("@passeport", p.Passeport);
                cmd.Parameters.AddWithValue("@nationalite", p.Nationalite);
                cmd.Parameters.AddWithValue("@telephone", p.Telephone);
                cmd.Parameters.AddWithValue("@idres", p.IdRes);
                cmd.ExecuteNonQuery();
            }
        }

        public void Modifier(Passager p)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("UPDATE passager SET nom=@nom, passeport=@passeport, nationalite=@nationalite, telephone=@telephone, idreserve=@idres WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@id", p.Id);
                cmd.Parameters.AddWithValue("@nom", p.Nom);
                cmd.Parameters.AddWithValue("@passeport", p.Passeport);
                cmd.Parameters.AddWithValue("@nationalite", p.Nationalite);
                cmd.Parameters.AddWithValue("@telephone", p.Telephone);
                cmd.Parameters.AddWithValue("@idres", p.IdRes);
                cmd.ExecuteNonQuery();
            }
        }

        public void Supprimer(string id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("DELETE FROM passager WHERE id=@id", conn);
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
                var cmd = new MySqlCommand("SELECT * FROM passager", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }
        public DataTable ObtenirTousRes(string res)
        {
            DataTable dt = new DataTable();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM passager WHERE idreserve=@id", conn);
                cmd.Parameters.AddWithValue("@id", res);
                using (var reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }

    }



}
