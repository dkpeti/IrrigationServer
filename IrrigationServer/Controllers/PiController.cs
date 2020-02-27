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
    //[Authorize]
    [Route("api/pi")]
    public class PiController : ControllerBase
    {
        private readonly IPiManager _piManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public PiController(IPiManager piManager, UserManager<User> userManager, IMapper mapper)
        {
            _piManager = piManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        // GET: api/Pi
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Pi> pies = _piManager.GetAll();
            return Ok(pies.Select(pies => _mapper.Map<PiDTO>(pies)));
        }

        // GET: api/Pi/5
        [HttpGet("{id}", Name = "GetPi")]
        public IActionResult Get(long id)
        {
            Pi pi = _piManager.Get(id);
            if(pi == null)
            {
                return NotFound("A Pi record nem talalhato");
            }
            return Ok(_mapper.Map<PiDTO>(pi));
        }

        // POST: api/Pi
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PiDTO pi)
        {
            if(pi ==null)
            {
                return BadRequest("Pi is null");
            }

            Pi newPi = _mapper.Map<Pi>(pi);
            newPi.User = await _userManager.FindByIdAsync("425760f0-4198-48b1-9d5a-5dd4e97c857d");
            _piManager.Add(newPi);
            return CreatedAtRoute(
                   "GetPi",
                   new { Id = newPi },
                   _mapper.Map<PiDTO>(newPi));
        }

        // PUT: api/Pi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] PiDTO pi)
        {
            if (pi == null)
            {
                return BadRequest("Pi is null.");
            }
            Pi piToUpdate = _piManager.Get(id);
            piToUpdate.User = await _userManager.FindByIdAsync("425760f0-4198-48b1-9d5a-5dd4e97c857d");
            if (piToUpdate == null)
            {
                return NotFound("A Pi record nem talalhato.");
            }
            _piManager.Update(piToUpdate, _mapper.Map<Pi>(pi));
            return NoContent();
        }

        // DELETE: api/Pi/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Pi pi = _piManager.Get(id);
            if(pi == null)
            {
                return NotFound("A Pi record nem talalhato.");
            }
            _piManager.Delete(pi);
            return NoContent();
        }

    }
}
