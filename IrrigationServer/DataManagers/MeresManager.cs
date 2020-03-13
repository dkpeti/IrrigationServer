using IrrigationServer.Context;
using IrrigationServer.Models;
using System.Collections.Generic;
using System.Linq;

namespace IrrigationServer.DataManagers
{
    public class MeresManager : IMeresManager
    {
        readonly IrrigationDbContext _irrigationContext;

        public MeresManager(IrrigationDbContext context)
        {
            _irrigationContext = context;
        }

        public IEnumerable<Meres> GetAllByPiIdAndZonaIdAndSzenzorId(string userId, long? piId = null, long? zonaId = null, long? szenzorId = null)
        {
            return _irrigationContext.Meresek
                .Where(meres => meres.Szenzor.Pi.User.Id == userId)
                .Where(meres => szenzorId == null || meres.Szenzor.Id == szenzorId)
                .Where(meres => zonaId == null || meres.Szenzor.Zona != null && meres.Szenzor.Zona.Id == zonaId)
                .Where(meres => piId == null || meres.Szenzor.Pi.Id == piId)
                .ToList();
        }

        public Meres Get(string userId, long id)
        {
            return _irrigationContext.Meresek
                .Where(meres => meres.Szenzor.Pi.User.Id == userId)
                .FirstOrDefault(e => e.Id == id);
        }

        public void Add(Meres entity)
        {
            _irrigationContext.Meresek.Add(entity);
            _irrigationContext.SaveChanges();
        }

        public void Update(Meres meres, Meres entity)
        {
            meres.Mikor = entity.Mikor;
            meres.MertAdat = entity.MertAdat;

            _irrigationContext.SaveChanges();
        }

        public void Delete(Meres meres)
        {
            _irrigationContext.Meresek.Remove(meres);
            _irrigationContext.SaveChanges();
        }

       
    }
}
