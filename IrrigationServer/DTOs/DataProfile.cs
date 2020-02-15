using AutoMapper;
using IrrigationServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrrigationServer.DTOs
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<Zona, ZonaDTO>().ReverseMap();
            CreateMap<Szenzor, SzenzorDTO>().ReverseMap();
            CreateMap<Meres, MeresDTO>().ReverseMap();
        }
    }
}
