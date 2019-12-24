using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace APIAeroport.Models
{
    public class Compagnie
    {
        [Key]
        public string CompagnieId { get; set; }
        public string NomCompagnie { get; set; }
        public string logoUri { get; set; }
    }
}
