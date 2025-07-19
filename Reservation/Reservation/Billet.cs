using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation
{
    public class Billet
    {
        public string Id { get; set; }
        public string Passager { get; set; }
        public string Vol { get; set; }
        public string Res { get; set; }
        public string Classe { get; set; }
        public int Siege { get; set; }
        public int Prix { get; set; }
        public string Etat { get; set; }

        public Billet(string id, string P, string V, string R, string C, int S, int prix, string etat)
        {
            Id = id;
            Passager = P;
            Vol = V;
            Res = R;
            Classe = C;
            Siege = S;
            Prix = prix;
            Etat = etat;
        }
    }
}
