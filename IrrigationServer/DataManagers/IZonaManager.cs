using IrrigationServer.Models;
using System.Collections.Generic;

namespace IrrigationServer.DataManagers
{
    public interface IZonaManager
    {
        void Add(Zona entity);
        void Delete(Zona zona);
        Zona Get(string userId, long id);
        IEnumerable<Zona> GetAllByPiId(string userId, long? piId = null);
        void Update(Zona zona, Zona entity);
    }
}