using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IrrigationServer.Models
{ 
    public enum SzenzorTipus
    {
        Homerseklet, Talajnedvesseg
    }
    public class Szenzor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Nev { get; set; }

        public SzenzorTipus Tipus { get; set; }

        public string Megjegyzes { get; set; }

        public virtual Zona Zona { get; set; }

        public virtual ICollection <Meres> Meresek { get; set; }
        public virtual Pi Pi { get; set; }
    }
}
