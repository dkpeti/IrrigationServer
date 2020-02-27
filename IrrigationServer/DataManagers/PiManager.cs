using IrrigationServer.Context;
using IrrigationServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrrigationServer.DataManagers
{
    public class PiManager : IPiManager
    {
        readonly IrrigationDbContext _irrigationContext;
        public PiManager(IrrigationDbContext context)
        {
            _irrigationContext = context;
        }
        public void Add(Pi entity)
        {
            _irrigationContext.Pies.Add(entity);
            _irrigationContext.SaveChanges();
        }

        public void Delete(Pi pi)
        {
            _irrigationContext.Pies.Remove(pi);
            _irrigationContext.SaveChanges();
        }

        public Pi Get(long id)
        {
            return _irrigationContext.Pies
                    .FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Pi> GetAll()
        {
            return _irrigationContext.Pies.ToList();
        }

        public void Update(Pi pi, Pi entity)
        {
            pi.Nev = entity.Nev;
            pi.Azonosito = entity.Azonosito;
            _irrigationContext.SaveChanges();
        }
    }
}
