using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TicketerAPI.Data;
using TicketerAPI.DTOs;
using TicketerAPI.Models;

namespace TicketerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrioridadController : ControllerBase
    {
        private readonly ITicketPrioridadRepo _repo;
        private readonly IMapper _mapper;

        public PrioridadController(ITicketPrioridadRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET /api/prioridad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketPrioridad>>> GetAllPrioridades()
        {
            return Ok(await _repo.GetAll());
        }

        // GET /api/prioridad/1
        [HttpGet("{id}", Name = "GetPrioridadById")]
        public async Task<ActionResult<TicketPrioridad>> GetPrioridadById(int id)
        {
            var prioridad = await _repo.GetById(id);

            if(prioridad == null)
            {
                return NotFound();
            }
            return Ok(prioridad);
        }

        // POST /api/prioridad
        [HttpPost]
        public async Task<ActionResult<TicketPrioridad>> CreatePrioridad(TicketStatusDTO statusDTO)
        {
            if(statusDTO.Color.Length > 7 || !statusDTO.Color.StartsWith("#"))
            {
                ModelState.AddModelError("ColorError", "Color should be in hexadecimal format and contain 7 characters including '#'. Ex: #00cdef.");
                return BadRequest(ModelState);
            }
            
            var prioridad = _mapper.Map<TicketPrioridad>(statusDTO);
            await _repo.CreateEntity(prioridad);
            await _repo.SaveChanges();

            return CreatedAtRoute(nameof(GetPrioridadById), new {id = prioridad.Id}, prioridad);
        }

        // PUT /api/prioridad/1
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePrioridad(int id, TicketStatusDTO statusDTO)
        {

            if(statusDTO.Color.Length > 7 || !statusDTO.Color.StartsWith("#"))
            {
                ModelState.AddModelError("ColorError", "Color should be in hexadecimal format and contain 7 characters including '#'. Ex: #00cdef.");
                return BadRequest(ModelState);
            }

            var prioridadFromRepo = await _repo.GetById(id);
            
            if(prioridadFromRepo == null)
                return NotFound();            

            _mapper.Map(statusDTO, prioridadFromRepo);

            _repo.UpdateEntity(prioridadFromRepo); 

            await _repo.SaveChanges();

            return NoContent();
        }

        // DELETE /api/prioridad/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePrioridad(int id)
        {
            var prioridadFromRepo = await _repo.GetById(id);
            if(prioridadFromRepo == null)
                return NotFound();

            _repo.DeleteEntity(prioridadFromRepo);
            await _repo.SaveChanges();

            return NoContent();
        }  
    }
}