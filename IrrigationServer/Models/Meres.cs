using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IrrigationServer.Models
{
    public class Meres
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public DateTime Mikor { get; set; }

        public long MertAdat { get; set; }

        public Szenzor Szenzor { get; set; }
    }
}
