using IrrigationServer.Models;
using System.Collections.Generic;

namespace IrrigationServer.DataManagers
{
    public interface ISzenzorManager
    {
        void Add(Szenzor entity);
        void Delete(Szenzor zona);
        Szenzor Get(string userId, long id);
        IEnumerable<Szenzor> GetAllByPiIdAndZonaId(string userId, long? piId = null, long? zonaId = null);
        IEnumerable<Szenzor> GetAllByPiIdAndInIdList(string userId, long piId, long[] idList);
        void Update(Szenzor zona, Szenzor entity);
    }
}