using AutoMapper;
using IrrigationServer.DataManagers;
using IrrigationServer.DTOs;
using IrrigationServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrrigationServer.Controllers
{
    [Authorize]
    [Route("api/szenzor")]
    [ApiController]
    public class SzenzorController : ControllerBase
    {
        private readonly IZonaManager _zonaManager;
        private readonly UserManager<User> _userManager;
        private readonly ISzenzorManager _szenzorManager;
        private readonly IMapper _mapper;

        public SzenzorController(IZonaManager zonaManager, UserManager<User> userManager, ISzenzorManager szenzorManager, IMapper mapper)
        {
            _zonaManager = zonaManager;
            _userManager = userManager;
            _szenzorManager = szenzorManager;
            _mapper = mapper;
        }

        // GET: api/Szenzor
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "piId")] long? piId = null, [FromQuery(Name = "zonaId")] long? zonaId = null)
        {
            User user = await _userManager.GetUserAsync(User);
            IEnumerable<Szenzor> szenzorok = _szenzorManager.GetAllByPiIdAndZonaId(user.Id, piId, zonaId);
            return Ok(szenzorok.Select(szenzor => _mapper.Map<SzenzorDTO>(szenzor)));
        }

        // GET: api/Szenzor/5
        [HttpGet("{id}", Name = "GetSzenzor")]
        public async Task<IActionResult> Get(int zonaId, long id)
        {
            User user = await _userManager.GetUserAsync(User);
            Szenzor szenzor = _szenzorManager.Get(user.Id, id);

            if (szenzor == null)
            {
                return NotFound("A Szenzor record nem talalhato.");
            }

            return Ok(_mapper.Map<SzenzorDTO>(szenzor));
        }

        // POST: api/Szenzor
        /*[HttpPost]
        public async Task<IActionResult> Post(int zonaId, [FromBody] SzenzorDTO szenzor)
        {
            if (szenzor == null)
            {
                return BadRequest("Szenzor is null.");
            }

            User user = await _userManager.GetUserAsync(User);
            Zona zona = _zonaManager.Get(user.Id, zonaId);
            Szenzor newSzenzor = _mapper.Map<Szenzor>(szenzor);
            newSzenzor.Zona = zona;
            _szenzorManager.Add(newSzenzor);
            return CreatedAtRoute(
                  "GetSzenzor",
                  new { ZonaId = zonaId, Id = newSzenzor.Id },
                  _mapper.Map<SzenzorDTO>(newSzenzor));
        }*/

        // PUT: api/Szenzor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int zonaId, long id, [FromBody] SzenzorDTO szenzor)
        {
            if (szenzor == null)
            {
                return BadRequest("Szenzor is null.");
            }

            User user = await _userManager.GetUserAsync(User);
            Szenzor szenzorToUpdate = _szenzorManager.Get(user.Id, id);
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
        public async Task<IActionResult> Delete(int zonaId, long id)
        {
            User user = await _userManager.GetUserAsync(User);
            Szenzor szenzor = _szenzorManager.Get(user.Id, id);
            if (szenzor == null)
            {
                return NotFound("A Szenzor record nem talalhato.");
            }

            _szenzorManager.Delete(szenzor);
            return NoContent();
        }
    }
}
