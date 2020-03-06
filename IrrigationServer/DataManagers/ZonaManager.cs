using IrrigationServer.Context;
using IrrigationServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrrigationServer.DataManagers
{
    public class ZonaManager : IZonaManager
    {
        readonly IrrigationDbContext _irrigationContext;

        public ZonaManager(IrrigationDbContext context)
        {
            _irrigationContext = context;
        }

        public IEnumerable<Zona> GetAll()
        {
            return _irrigationContext.Zonak.ToList();
        }

        public Zona Get(long id)
        {
            return _irrigationContext.Zonak
                    .FirstOrDefault(e => e.Id == id);
        }

        public void Add(Zona entity)
        {
            _irrigationContext.Zonak.Add(entity);
            _irrigationContext.SaveChanges();
        }

        public void Update(Zona zona, Zona entity)
        {
            zona.Nev = entity.Nev;
            zona.Szenzorok = entity.Szenzorok;
            zona.Pi = entity.Pi;

            _irrigationContext.SaveChanges();
        }

        public void Delete(Zona zona)
        {
            _irrigationContext.Zonak.Remove(zona);
            _irrigationContext.SaveChanges();
        }
    }
}
