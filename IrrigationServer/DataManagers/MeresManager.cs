using IrrigationServer.Context;
using IrrigationServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrrigationServer.DataManagers
{
    public class MeresManager : IMeresManager
    {
        readonly IrrigationDbContext _irrigationContext;

        public MeresManager(IrrigationDbContext context)
        {
            _irrigationContext = context;
        }

        public IEnumerable<Meres> GetAll()
        {
            return _irrigationContext.Meresek.ToList();
        }

        public Meres Get(long id)
        {
            return _irrigationContext.Meresek
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
