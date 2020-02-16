using AutoMapper;
using IrrigationServer.DataManagers;
using IrrigationServer.DTOs;
using IrrigationServer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace IrrigationServer.Controllers
{
    [Route("api/zona/{zonaId:int}/szenzor")]
    [ApiController]
    public class SzenzorController : ControllerBase
    {
        private readonly IZonaManager _zonaManager;
        private readonly ISzenzorManager _szenzorManager;
        private readonly IMapper _mapper;

        public SzenzorController(IZonaManager zonaManager, ISzenzorManager szenzorManager, IMapper mapper)
        {
            _zonaManager = zonaManager;
            _szenzorManager = szenzorManager;
            _mapper = mapper;
        }

        // GET: api/Szenzor
        [HttpGet]
        public IActionResult Get(int zonaId)
        {
            //
            IEnumerable<Szenzor> szenzorok = _szenzorManager.GetAllByZonaId(zonaId);
            return Ok(szenzorok.Select(szenzor => _mapper.Map<SzenzorDTO>(szenzor)));
        }

        // GET: api/Szenzor/5
        [HttpGet("{id}", Name = "GetSzenzor")]
        public IActionResult Get(int zonaId, long id)
        {
            Szenzor szenzor = _szenzorManager.Get(id);

            if (szenzor == null)
            {
                return NotFound("A Szenzor record nem talalhato.");
            }

            return Ok(_mapper.Map<SzenzorDTO>(szenzor));
        }

        // POST: api/Szenzor
        [HttpPost]
        public IActionResult Post(int zonaId, [FromBody] SzenzorDTO szenzor)
        {
            if (szenzor == null)
            {
                return BadRequest("Szenzor is null.");
            }
            //
            Zona zona = _zonaManager.Get(zonaId);
            Szenzor newSzenzor = _mapper.Map<Szenzor>(szenzor);
            newSzenzor.Zona = zona;
            _szenzorManager.Add(newSzenzor);
            return CreatedAtRoute(
                  "GetSzenzor",
                  new { ZonaId = zonaId, Id = newSzenzor.Id },
                  _mapper.Map<SzenzorDTO>(newSzenzor));
        }

        // PUT: api/Szenzor/5
        [HttpPut("{id}")]
        public IActionResult Put(int zonaId, long id, [FromBody] SzenzorDTO szenzor)
        {
            if (szenzor == null)
            {
                return BadRequest("Szenzor is null.");
            }

            Szenzor szenzorToUpdate = _szenzorManager.Get(id);
            if (szenzorToUpdate == null)
            {
                return NotFound("A Szenzor record nem talalhato.");
            }
            //
            Szenzor updatedSzenzor = _mapper.Map<Szenzor>(szenzor);
            updatedSzenzor.Zona = szenzorToUpdate.Zona;
            _szenzorManager.Update(szenzorToUpdate, updatedSzenzor);
            return NoContent();
        }

        // DELETE: api/Szenzor/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int zonaId, long id)
        {
            Szenzor szenzor = _szenzorManager.Get(id);
            if (szenzor == null)
            {
                return NotFound("A Szenzor record nem talalhato.");
            }

            _szenzorManager.Delete(szenzor);
            return NoContent();
        }
    }
}
