using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIAeroport
{
    public class Departures
    {
        public int VolCeduleId { get; set; }
        public string DatePrevue { get; set; }
        public string HeurePrevue { get; set; }
        public string HeureRevisee { get; set; }
        public string CompLogo { get; set; }
        public string NumVol { get; set; }
        public string Destination { get; set; }
        public int Statut { get; set; }
        public string Porte { get; set; }
    }
}
