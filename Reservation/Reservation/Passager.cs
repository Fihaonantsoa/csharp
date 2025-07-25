using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reservation
{
    public class Passager
    {
        public string Id { get; set; }
        public string Nom { get; set; }
        public string Passeport { get; set; }
        public string Nationalite { get; set; }
        public string Telephone { get; set; }
        public string IdRes { get; set; }

        public Passager(string id, string nom, string passeport, string nationalite, string telephone, string idres)
        {
            Id = id;
            Nom = nom;
            Passeport = passeport;
            Nationalite = nationalite;
            Telephone = telephone;
            IdRes = idres;
        }
    }
}