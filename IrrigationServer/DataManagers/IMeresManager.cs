using IrrigationServer.Models;
using System.Collections.Generic;

namespace IrrigationServer.DataManagers
{
    public interface IMeresManager
    {
        void Add(Meres entity);
        void Delete(Meres zona);
        Meres Get(string userId, long id);
        IEnumerable<Meres> GetAllByPiIdAndZonaIdAndSzenzorId(string userId, long? piId = null, long? zonaId = null, long? szenzorId = null);
        void Update(Meres zona, Meres entity);

    }
}