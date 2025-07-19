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
    public class Vol
    {
        public string Id { get; set; }
        public DateTime Datedep { get; set; }
        public DateTime Datearr { get; set; }
        public string Statut { get; set; }
        public Vol(string id, DateTime datedep, DateTime datearr, string statut)
        {
            Id = id;
            Datedep = datedep;
            Datearr = datearr;
            Statut = statut;
        }
    }
}