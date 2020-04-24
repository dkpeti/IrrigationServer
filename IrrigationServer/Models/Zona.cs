using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IrrigationServer.Models
{
    public class Zona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Nev { get; set; }

        public DateTime? UtolsoOntozesKezdese { get; set; }
        public int? UtolsoOntozesHossza { get; set; }

        public virtual ICollection<Szenzor> Szenzorok { get; set; }
        public virtual Pi Pi { get; set; }
    }
}
