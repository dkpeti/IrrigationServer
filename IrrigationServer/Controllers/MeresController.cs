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
    [Route("api/meres")]
    [ApiController]
    public class MeresController : ControllerBase
    {
        private readonly IMeresManager _meresManager;
        private readonly IMapper _mapper;

        public MeresController(IMeresManager meresManager, IMapper mapper)
        {
            _meresManager = meresManager;
            _mapper = mapper;
        }

        // GET: api/Meres
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Meres> meresek = _meresManager.GetAll();
            return Ok(meresek.Select(meres => _mapper.Map<MeresDTO>(meres)));
        }

        // GET: api/Meres/5
        [HttpGet("{id}")]
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
        public IActionResult Post([FromBody] MeresDTO meres)
        {
            if (meres == null)
            {
                return BadRequest("Meres is null.");
            }

            Meres newMeres = _mapper.Map<Meres>(meres);
            _meresManager.Add(newMeres);
            return CreatedAtRoute(
                  "Get",
                  new { Id = newMeres.Id },
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

            _meresManager.Update(meresToUpdate, _mapper.Map<Meres>(meres));
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
