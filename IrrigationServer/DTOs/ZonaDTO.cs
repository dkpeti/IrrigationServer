using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IrrigationServer.DTOs
{
    public class ZonaDTO
    {
        public long Id { get; set; }
        public long PiId { get; set; }
        public string Nev { get; set; }
        public long[] SzenzorLista { get; set; }
        public DateTime UtolsoOntozesKezdese { get; set; }
        public int? UtolsoOntozesHossza { get; set; }
    }
}
