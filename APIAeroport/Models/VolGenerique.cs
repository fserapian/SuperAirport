using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIAeroport.Models
{
    public class VolGenerique
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string VolGeneriqueId { get; set; }
        public string AeroportId { get; set; }
        public string CompagnieId { get; set; }
        public DateTime HeurePrevue { get; set; }
        public bool Direction { get; set; }
    }
}
