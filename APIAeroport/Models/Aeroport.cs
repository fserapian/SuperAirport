using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace APIAeroport.Models
{
    public class Aeroport
    {
        [Key]
        public string AeroportId { get; set; }
        public string NomAeroport { get; set; }
        public string Ville { get; set; }
        public string Pays { get; set; }
    }
}
