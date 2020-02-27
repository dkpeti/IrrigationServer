using IrrigationServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrrigationServer.Authentication
{
    interface IAuthService
    {
        Task<User> Authenticate(UserTokenId token);
    }
}
