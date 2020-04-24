using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrrigationServer.DTOs
{
    public enum OntozesUtasitas
    {
        KEZDES, VEGE
    }

    public class OntozesDTO
    {
        public OntozesUtasitas Utasitas { get; set; }
        public int? Hossz { get; set; }
    }
}
