using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIAeroport.Models
{
    public class VolCedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VolCeduleId { get; set; }
        public string VolGeneriqueId { get; set; }
        public DateTime DatePrevue { get; set; }
        public DateTime DateRevisee { get; set; }
        public int Statut { get; set; }
        public string Porte { get; set; }
    }
}
