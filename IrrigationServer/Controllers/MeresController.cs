using AutoMapper;
using IrrigationServer.DataManagers;
using IrrigationServer.DTOs;
using IrrigationServer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrrigationServer.Controllers
{
    [Route("api/zona/{zonaId:int}/szenzor/{szenzorId:int}/meres")]
    [ApiController]
    public class MeresController : ControllerBase
    {
        private readonly IZonaManager _zonaManager;
        private readonly ISzenzorManager _szenzorManager;
        private readonly IMeresManager _meresManager;
        private readonly IMapper _mapper;

        public MeresController(IZonaManager zonaManager,ISzenzorManager szenzorManager, IMeresManager meresManager, IMapper mapper)
        {
            _zonaManager = zonaManager;
            _szenzorManager = szenzorManager;
            _meresManager = meresManager;
            _mapper = mapper;
        }

        // GET: api/Meres
        [HttpGet]
        public IActionResult Get(int zonaId, int szenzorId)
        {
            IEnumerable<Szenzor> szenzorok =_szenzorManager.GetAllByZonaId(zonaId);
            IEnumerable<Meres> meresek = _meresManager.GetAllBySzenzorId(szenzorId);
            return Ok(meresek.Select(meres => _mapper.Map<MeresDTO>(meres)));
        }

        // GET: api/Meres/5
        [HttpGet("{id}", Name ="GetMeres") ]
        public IActionResult Get(long id)
        {
            Meres meres = _meresManager.Get(id);

            if (meres == null)
            {
                return NotFound("A Meres record nem talalhato.");
            }

            return Ok(_mapper.Map<MeresDTO>(meres));
        }

        // POST: api/Meres
        [HttpPost]
        public IActionResult Post(int szenzorId, int zonaId, [FromBody] MeresDTO meres)
        {
            if (meres == null)
            {
                return BadRequest("Meres is null.");
            }
            Szenzor szenzor = _szenzorManager.Get(szenzorId);        
            Meres newMeres = _mapper.Map<Meres>(meres);
            newMeres.Szenzor = szenzor;
            _meresManager.Add(newMeres);
            return CreatedAtRoute(
                  "GetMeres",
                  new { ZonaId = zonaId, SzenzorId = szenzorId, Id = newMeres.Id },
                  _mapper.Map<MeresDTO>(newMeres));
        }

        // PUT: api/Meres/5 
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] MeresDTO meres)
        {
            if (meres == null)
            {
                return BadRequest("Meres is null.");
            }

            Meres meresToUpdate = _meresManager.Get(id);
            if (meresToUpdate == null)
            {
                return NotFound("A Meres record nem talalhato.");
            }
            Meres updatedMeres = _mapper.Map<Meres>(meres);
            updatedMeres.Szenzor = meresToUpdate.Szenzor;
            _meresManager.Update(meresToUpdate, updatedMeres);
            return NoContent();
        }

        // DELETE: api/Meres/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Meres meres = _meresManager.Get(id);
            if (meres == null)
            {
                return NotFound("A Meres record nem talalhato.");
            }

            _meresManager.Delete(meres);
            return NoContent();
        }
    }
}
