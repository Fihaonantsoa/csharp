using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation
{
    public class Reservation
    {
        public string Id { get; set; }
        public DateTime Dateres { get; set; }
        public int Prix { get; set; }
        public string ModePaie { get; set; }

        public Reservation(string id, DateTime dateres, int prix, string modePaie)
        {
            Id = id;
            Dateres = dateres;
            Prix = prix;
            ModePaie = modePaie;
        }
    }
}
