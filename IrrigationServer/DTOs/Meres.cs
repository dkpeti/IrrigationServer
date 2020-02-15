using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IrrigationServer.DTOs
{
    public class MeresDTO
    {
        public long Id { get; set; }

        public DateTime Mikor { get; set; }

        public long MertAdat { get; set; }
    }
}
