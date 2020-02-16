using IrrigationServer.Models;
using System.Collections.Generic;

namespace IrrigationServer.DataManagers
{
    public interface ISzenzorManager
    {
        void Add(Szenzor entity);
        void Delete(Szenzor zona);
        Szenzor Get(long id);
        IEnumerable<Szenzor> GetAll();
        IEnumerable<Szenzor> GetAllByZonaId(long zonaId);
        void Update(Szenzor zona, Szenzor entity);
    }
}