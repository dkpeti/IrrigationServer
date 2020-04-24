using IrrigationServer.Context;
using IrrigationServer.Hubs;
using IrrigationServer.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrrigationServer.DataManagers
{
    public class PiManager : IPiManager
    {
        private readonly IrrigationDbContext _irrigationContext;

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

        public Pi Get(string userId, long id)
        {
            return _irrigationContext.Pies
                .Where(pi => pi.User.Id == userId)
                .FirstOrDefault(e => e.Id == id);
        }
        public Pi GetByAzonosito(string azonosito)
        {
            return _irrigationContext.Pies
                .Where(pi => pi.Azonosito == azonosito)
                .FirstOrDefault();
        }

        public IEnumerable<Pi> GetAll(string userId)
        {
            return _irrigationContext.Pies
                .Where(pi => pi.User.Id == userId)
                .ToList();
        }

        public void Update(Pi pi, Pi entity)
        {
            pi.Nev = entity.Nev;
            pi.Azonosito = entity.Azonosito;
            _irrigationContext.SaveChanges();
        }
    }
}
