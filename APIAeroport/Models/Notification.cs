using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIAeroport.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VolCeduleId { get; set; }
        public string NumTel { get; set; }
        public DateTime DateInscription { get; set; }
        public DateTime? DateArret { get; set; }
    }
}
