using IrrigationServer.Context;
using IrrigationServer.Models;
using System.Collections.Generic;
using System.Linq;

namespace IrrigationServer.DataManagers
{
    public class SzenzorManager : ISzenzorManager
    {
        readonly IrrigationDbContext _irrigationContext;

        public SzenzorManager(IrrigationDbContext context)
        {
            _irrigationContext = context;
        }

        /**/
        public IEnumerable<Szenzor> GetAllByPiIdAndZonaId(string userId, long? piId = null, long? zonaId = null)
        {
            return _irrigationContext.Szenzorok
                .Where(szenzor => szenzor.Pi.User.Id == userId)
                .Where(szenzor => zonaId == null || szenzor.Zona != null && szenzor.Zona.Id == zonaId)
                .Where(szenzor => piId == null || szenzor.Pi.Id == piId)
                .ToList();
        }

        public Szenzor Get(string userId, long id)
        {
            return _irrigationContext.Szenzorok
                .Where(szenzor => szenzor.Pi.User.Id == userId)
                .FirstOrDefault(e => e.Id == id);
        }

        public void Add(Szenzor entity)
        {
            _irrigationContext.Szenzorok.Add(entity);
            _irrigationContext.SaveChanges();
        }

        public void Update(Szenzor szenzor, Szenzor entity)
        {
            szenzor.Nev = entity.Nev;
            szenzor.Tipus = entity.Tipus;
            szenzor.Megjegyzes = entity.Megjegyzes;

            _irrigationContext.SaveChanges();
        }

        public void Delete(Szenzor szenzor)
        {
            _irrigationContext.Szenzorok.Remove(szenzor);
            _irrigationContext.SaveChanges();
        }
    }
}
