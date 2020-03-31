using AutoMapper;
using IrrigationServer.DataManagers;
using IrrigationServer.DTOs;
using IrrigationServer.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrrigationServer.Hubs
{
    public interface IPiClient
    {
        public Task PiLoginFailed();
        public Task PiLoginSucceeded();

        public Task RegisterSensorFailed(SzenzorDTO szenzorDTO);
        public Task RegisterSensorSucceeded(SzenzorDTO szenzorDTO, long id);
    }

    public class PiHub : Hub<IPiClient>
    {
        private readonly IMapper _mapper;
        private readonly ISzenzorManager _szenzorManager;
        private readonly IPiManager _piManager;

        private Dictionary<string, string> _azonositoToConnectionId;

        public PiHub(ISzenzorManager szenzorManager, IPiManager piManager, IMapper mapper)
        {
            _szenzorManager = szenzorManager;
            _piManager = piManager;
            _mapper = mapper;

            _azonositoToConnectionId = new Dictionary<string, string>();
        }

        public async Task PiLogin(string azonosito)
        {
            try
            {
                Pi pi = _piManager.GetByAzonosito(azonosito);
                if (pi == null)
                {
                    await Clients.Caller.PiLoginFailed();
                }
                else
                {
                    _azonositoToConnectionId.Add(azonosito, Context.ConnectionId);
                    Context.Items.Add("piId", azonosito);
                    await Clients.Caller.PiLoginSucceeded();
                }
            }
            catch (Exception)
            {
                await Clients.Caller.PiLoginFailed();
                throw;
            }
        }

        public async Task RegisterSensor(SzenzorDTO szenzorDTO)
        {
            try
            {
                Pi pi = _piManager.GetByAzonosito(Context.Items["piId"] as string);
                Szenzor szenzor = _mapper.Map<Szenzor>(szenzorDTO);

                if (pi == null || szenzor == null)
                {
                    await Clients.Caller.RegisterSensorFailed(szenzorDTO);
                }
                else
                {
                    szenzor.Pi = pi;
                    _szenzorManager.Add(szenzor);
                    await Clients.Caller.RegisterSensorSucceeded(szenzorDTO, szenzor.Id);
                }
            }
            catch (Exception)
            {
                await Clients.Caller.RegisterSensorFailed(szenzorDTO);
                throw;
            }
        }
    }
}
