using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation
{
    public class Paiement
    {
        public string Id { get; set; }
        public string IDres { get; set; }
        public int Montant { get; set; }
        public DateTime datePaie { get; set; }

        public Paiement(string id, string idrese, int montant, DateTime date)
        {
            Id = id;
            IDres = idrese;
            Montant = montant;
            datePaie = date;
        }
    }
}
