using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IrrigationServer.DTOs
{ 
    public enum SzenzorTipus
    {
        Homerseklet, Talajnedvesseg
    }
    public class SzenzorDTO
    {
        public long Id { get; set; }

        public string Nev { get; set; }

        public SzenzorTipus Tipus { get; set; }

        public string Megjegyzes { get; set; }
    }
}
