﻿using AutoMapper;
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
    }

    public class PiHub : Hub<IPiClient>
    {
        private readonly IMapper _mapper;
        private readonly ISzenzorManager _szenzorManager;
        private readonly IMeresManager _meresManager;
        private readonly IPiManager _piManager;

        private Dictionary<string, string> _azonositoToConnectionId;

        public PiHub(ISzenzorManager szenzorManager, IMeresManager meresManager, IPiManager piManager, IMapper mapper)
        {
            _szenzorManager = szenzorManager;
            _meresManager = meresManager;
            _piManager = piManager;
            _mapper = mapper;

            _azonositoToConnectionId = new Dictionary<string, string>();
        }

        public async Task<PiLoginResponseDTO> PiLogin(string azonosito)
        {
            try
            {
                Pi pi = _piManager.GetByAzonosito(azonosito);
                if (pi == null)
                {
                    return new PiLoginResponseDTO() { Success = false };
                }
                else
                {
                    _azonositoToConnectionId.Add(azonosito, Context.ConnectionId);
                    Context.Items.Add("piId", azonosito);
                    return new PiLoginResponseDTO() { Success = true };
                }
            }
            catch (Exception)
            {
                return new PiLoginResponseDTO() { Success = false };
            }
        }

        public async Task<RegisterSensorResponseDTO> RegisterSensor(SzenzorDTO szenzorDTO)
        {
            try
            {
                Pi pi = _piManager.GetByAzonosito(Context.Items["piId"] as string);
                Szenzor szenzor = _mapper.Map<Szenzor>(szenzorDTO);

                if (pi == null || szenzor == null)
                {
                    return new RegisterSensorResponseDTO() { Success = false };
                }
                else
                {
                    szenzor.Pi = pi;
                    _szenzorManager.Add(szenzor);
                    return new RegisterSensorResponseDTO() { Success = true, Id = szenzor.Id };
                }
            }
            catch (Exception)
            {
                return new RegisterSensorResponseDTO() { Success = false };
            }
        }

        public async Task<PostMeresDataResponseDTO> PostMeresData(PostMeresDataDTO meresDTO)
        {
            try
            {
                Pi pi = _piManager.GetByAzonosito(Context.Items["piId"] as string);
                Szenzor szenzor = _szenzorManager.GetOneByPiIdAndId(pi.Id, meresDTO.SzenzorId);
                Meres meres = new Meres()
                {
                    MertAdat = meresDTO.MertAdat,
                    Mikor = meresDTO.Mikor,
                    Szenzor = szenzor
                };

                if (pi == null || meres == null || szenzor == null)
                {
                    return new PostMeresDataResponseDTO() { Success = false };
                }
                else
                {
                    _meresManager.Add(meres);
                    return new PostMeresDataResponseDTO() { Success = true };
                }
            }
            catch (Exception)
            {
                return new PostMeresDataResponseDTO() { Success = false };
            }
        }
    }
}
