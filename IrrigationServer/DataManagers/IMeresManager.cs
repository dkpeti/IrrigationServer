using IrrigationServer.Models;
using System.Collections.Generic;

namespace IrrigationServer.DataManagers
{
    public interface IMeresManager
    {
        void Add(Meres entity);
        void Delete(Meres zona);
        Meres Get(long id);
        IEnumerable<Meres> GetAll();
        IEnumerable<Szenzor> GetAllByZonaId(long zonaId);
        IEnumerable<Meres> GetAllBySzenzorId(long szenzorId);
        void Update(Meres zona, Meres entity);

    }
}