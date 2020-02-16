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
        void Update(Meres zona, Meres entity);
    }
}