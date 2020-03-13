using AutoMapper;
using IrrigationServer.DataManagers;
using IrrigationServer.DTOs;
using IrrigationServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IPiManager _piManager;
        private readonly IMapper _mapper;

        public ZonaController(IZonaManager zonaManager, UserManager<User> userManager, IPiManager piManager, IMapper mapper)
        {
            _zonaManager = zonaManager;
            _userManager = userManager;
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
                return NotFound("A Zona record nem talalhato.");
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
                return BadRequest("Pi does not exist");
            }
    
            Zona newZona = _mapper.Map<Zona>(zona);
            newZona.Pi = pi;
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
                return BadRequest("Pi does not exist");
            }

            Zona zonaToUpdate = _zonaManager.Get(user.Id, id);
            if (zonaToUpdate == null)
            {
                return NotFound("A Zona record nem talalhato.");
            }

            Zona updatedZona = _mapper.Map<Zona>(zona);
            updatedZona.Pi = pi;

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
                return NotFound("A Zona record nem talalhato.");
            }

            _zonaManager.Delete(zona);
            return NoContent();
        }
    }
}
