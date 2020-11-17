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
    public class StatusController : ControllerBase
    {
        private readonly ITicketStatusRepo _repo;
        private readonly IMapper _mapper;

        public StatusController(ITicketStatusRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET /api/servicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketStatus>>> GetAllStatus()
        {
            return Ok(await _repo.GetAll());
        }

        // GET /api/servicios/1
        [HttpGet("{id}", Name = "GetStatusById")]
        public async Task<ActionResult<TicketStatus>> GetStatusById(int id)
        {
            var status = await _repo.GetById(id);

            if(status == null)
            {
                return NotFound();
            }
            return Ok(status);
        }

        // POST /api/servicios/1
        [HttpPost]
        public async Task<ActionResult<TicketStatus>> CreateStatus(TicketStatusDTO statusDTO)
        {
            if(statusDTO.Color.Length > 7 || !statusDTO.Color.StartsWith("#"))
            {
                ModelState.AddModelError("ColorError", "Color should be in hexadecimal format and contain 7 characters including '#'. Ex: #00cdef.");
                return BadRequest(ModelState);
            }

            var status = _mapper.Map<TicketStatus>(statusDTO);
            await _repo.CreateEntity(status);
            await _repo.SaveChanges();

            return CreatedAtRoute(nameof(GetStatusById), new {id = status.Id}, status);
        }

        // PUT /api/servicios/1
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStatus(int id, TicketStatusDTO statusDTO)
        {
            if(statusDTO.Color.Length > 7 || !statusDTO.Color.StartsWith("#"))
            {
                ModelState.AddModelError("ColorError", "Color should be in hexadecimal format and contain 7 characters including '#'. Ex: #00cdef.");
                return BadRequest(ModelState);
            }

            var StatusFromRepo = await _repo.GetById(id);

            if(StatusFromRepo == null)
                return NotFound();

            _mapper.Map(statusDTO, StatusFromRepo);

            _repo.UpdateEntity(StatusFromRepo); 

            await _repo.SaveChanges();

            return NoContent();
        }

        // DELETE /api/servicios/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStatus(int id)
        {
            var StatusFromRepo = await _repo.GetById(id);
            if(StatusFromRepo == null)
                return NotFound();

            _repo.DeleteEntity(StatusFromRepo);
            await _repo.SaveChanges();

            return NoContent();
        }  
    }
}