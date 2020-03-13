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
        Pi Get(string userId, long id);
        IEnumerable<Pi> GetAll(string userId);
        void Update(Pi pi, Pi entity);
    }
}
