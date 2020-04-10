using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrrigationServer.DTOs
{
    public class PostMeresDataDTO
    {
        public long SzenzorId { get; set; }

        public DateTime Mikor { get; set; }

        public long MertAdat { get; set; }
    }
}
