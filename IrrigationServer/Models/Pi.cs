using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IrrigationServer.Models
{
    public class Pi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Nev { get; set; }
        public string Azonosito { get; set; }
        public virtual ICollection<Zona> Zonak { get; set; }
        public virtual ICollection<Szenzor>Szenzorok { get; set; }
        public virtual User User { get; set; }
    }
}
