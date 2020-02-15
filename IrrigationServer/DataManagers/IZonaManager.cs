using IrrigationServer.Models;
using System.Collections.Generic;

namespace IrrigationServer.DataManagers
{
    public interface IZonaManager
    {
        void Add(Zona entity);
        void Delete(Zona zona);
        Zona Get(long id);
        IEnumerable<Zona> GetAll();
        void Update(Zona zona, Zona entity);
    }
}