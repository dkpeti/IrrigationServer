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

        public IEnumerable<Szenzor> GetAll()
        {
            return _irrigationContext.Szenzorok.ToList();
        }

        /**/
        public IEnumerable<Szenzor> GetAllByZonaId(long zonaId)
        {
            return _irrigationContext.Szenzorok.Where(szenzor => szenzor.Zona.Id == zonaId).ToList();
        }

        public Szenzor Get(long id)
        {
            return _irrigationContext.Szenzorok
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
