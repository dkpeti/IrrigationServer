using AutoMapper;
using IrrigationServer.DataManagers;
using IrrigationServer.DTOs;
using IrrigationServer.Hubs;
using IrrigationServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IrrigationServer.Controllers
{
    [Authorize]
    [Route("api/zona")]
    [ApiController]
    public class ZonaController : ControllerBase
    {
        private readonly IZonaManager _zonaManager;
        private readonly UserManager<User> _userManager;
        private readonly IHubContext<PiHub> _piHub;
        private readonly ISzenzorManager _szenzorManager;
        private readonly IPiManager _piManager;
        private readonly IMapper _mapper;

        public ZonaController(IZonaManager zonaManager, UserManager<User> userManager, IHubContext<PiHub> piHub, ISzenzorManager szenzorManager, IPiManager piManager, IMapper mapper)
        {
            _zonaManager = zonaManager;
            _userManager = userManager;
            _piHub = piHub;
            _szenzorManager = szenzorManager;
            _piManager = piManager;
            _mapper = mapper;
        }

        // GET: api/Zona
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "piId")] long? piId = null)
        {
            User user = await _userManager.GetUserAsync(User);
            IEnumerable<Zona> zonak = _zonaManager.GetAllByPiId(user.Id, piId);
            return Ok(zonak.Select(zona => _mapper.Map<ZonaDTO>(zona)));
        }

        // GET: api/Zona/5
        [HttpGet("{id}", Name = "GetZona")]
        public async Task<IActionResult> Get(long id)
        {
            User user = await _userManager.GetUserAsync(User);
            Zona zona = _zonaManager.Get(user.Id, id);

            if (zona == null)
            {
                return NotFound("A Zona record nem található.");
            }

            return Ok(_mapper.Map<ZonaDTO>(zona));
        }

        // POST: api/Zona
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ZonaDTO zona)
        {
            if (zona == null)
            {
                return BadRequest("Zona is null.");
            }

            User user = await _userManager.GetUserAsync(User);
            Pi pi = _piManager.Get(user.Id, zona.PiId);
            if(pi == null)
            {
                return BadRequest("A pi nem létezik");
            }
    
            Zona newZona = _mapper.Map<Zona>(zona);
            newZona.Pi = pi;
            newZona.Szenzorok = _szenzorManager.GetAllByPiIdAndInIdList(user.Id, pi.Id, zona.SzenzorLista).ToList();
            _zonaManager.Add(newZona);
            return CreatedAtRoute(
                  "GetZona",
                  new { Id = newZona.Id },
                  _mapper.Map<ZonaDTO>(newZona));
        }

        // PUT: api/Zona/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] ZonaDTO zona)
        {
            if (zona == null)
            {
                return BadRequest("Zona is null.");
            }

            User user = await _userManager.GetUserAsync(User);
            Pi pi = _piManager.Get(user.Id, zona.PiId);
            if (pi == null)
            {
                return BadRequest("A pi nem létezik");
            }

            Zona zonaToUpdate = _zonaManager.Get(user.Id, id);
            if (zonaToUpdate == null)
            {
                return NotFound("A Zona record nem található.");
            }

            Zona updatedZona = _mapper.Map<Zona>(zona);
            updatedZona.Pi = pi;
            updatedZona.Szenzorok = zonaToUpdate.Szenzorok;
            updatedZona.Szenzorok.Clear();
            foreach(var szenzor in _szenzorManager.GetAllByPiIdAndInIdList(user.Id, pi.Id, zona.SzenzorLista))
            {
                updatedZona.Szenzorok.Add(szenzor);
            }

            _zonaManager.Update(zonaToUpdate, updatedZona);
            return NoContent();
        }

        // DELETE: api/Zona/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            User user = await _userManager.GetUserAsync(User);
            Zona zona = _zonaManager.Get(user.Id, id);
            if (zona == null)
            {
                return NotFound("A Zona record nem található.");
            }

            _zonaManager.Delete(zona);
            return NoContent();
        }

        [HttpPost("{id}/ontozes")]
        public async Task<IActionResult> PostOntozes(long id, [FromBody] OntozesDTO ontozes)
        {
            User user = await _userManager.GetUserAsync(User);
            Zona zona = _zonaManager.Get(user.Id, id);

            if (zona == null)
            {
                return NotFound("A Zona record nem található.");
            }

            string connectionId = PiHub.GetConnectionIdForAzonosito(zona.Pi.Azonosito);
            if(connectionId == null)
            {
                return NotFound("A pi nem érhető el");
            }

            await _piHub.Clients.Client(connectionId).SendCoreAsync("Ontozes", new object[] { ontozes });
            var waitForResponse = new AutoResetEvent(false);
            bool piOK = false;
            void Callback(string connId, bool isSuccessful)
            {
                if (connectionId == connId)
                {
                    piOK = isSuccessful;
                    waitForResponse.Set();
                }
            }
            PiHub.OntozesResponse += Callback;
            waitForResponse.WaitOne(TimeSpan.FromSeconds(10));
            PiHub.OntozesResponse -= Callback;

            if(piOK)
            {
                if(ontozes.Utasitas == OntozesUtasitas.KEZDES)
                {
                    zona.UtolsoOntozesKezdese = DateTime.UtcNow;
                    zona.UtolsoOntozesHossza = ontozes.Hossz ?? 0;
                }
                else if(ontozes.Utasitas == OntozesUtasitas.VEGE)
                {
                    zona.UtolsoOntozesKezdese = null;
                    zona.UtolsoOntozesHossza = null;
                }
                _zonaManager.UpdateOntozes(zona);
                return Ok();
            }
            else
            {
                return NotFound("A pi nem érhető el");
            }
        }
    }
}
