using IrrigationServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrrigationServer.DataManagers
{
    public interface IPiManager
    {
        void Add(Pi entity);
        void Delete(Pi pi);
        Pi Get(long id);
        IEnumerable<Pi> GetAll();
        void Update(Pi pi, Pi entity);
    }
}
