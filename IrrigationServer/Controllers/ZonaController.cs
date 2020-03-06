using AutoMapper;
using IrrigationServer.DataManagers;
using IrrigationServer.DTOs;
using IrrigationServer.Models;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IPiManager _piManager;
        private readonly IMapper _mapper;

        public ZonaController(IZonaManager zonaManager, IPiManager piManager, IMapper mapper)
        {
            _zonaManager = zonaManager;
            _piManager = piManager;
            _mapper = mapper;
        }

        // GET: api/Zona
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Zona> zonak = _zonaManager.GetAll();
            return Ok(zonak.Select(zona => _mapper.Map<ZonaDTO>(zona)));

        }

        // GET: api/Zona/5
        [HttpGet("{id}", Name = "GetZona")]
        public IActionResult Get(long id)
        {
            Zona zona = _zonaManager.Get(id);

            if (zona == null)
            {
                return NotFound("A Zona record nem talalhato.");
            }

            return Ok(_mapper.Map<ZonaDTO>(zona));
        }

        // POST: api/Zona
        [HttpPost]
        public IActionResult Post([FromBody] ZonaDTO zona)
        {
            if (zona == null)
            {
                return BadRequest("Zona is null.");
            }

            Pi pi = _piManager.Get(zona.PiId);
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
        public IActionResult Put(long id, [FromBody] ZonaDTO zona)
        {
            if (zona == null)
            {
                return BadRequest("Zona is null.");
            }

            Pi pi = _piManager.Get(zona.PiId);
            if (pi == null)
            {
                return BadRequest("Pi does not exist");
            }

            Zona zonaToUpdate = _zonaManager.Get(id);
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
        public IActionResult Delete(long id)
        {
            Zona zona = _zonaManager.Get(id);
            if (zona == null)
            {
                return NotFound("A Zona record nem talalhato.");
            }

            _zonaManager.Delete(zona);
            return NoContent();
        }
    }
}
