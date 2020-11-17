using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TicketerAPI.Data;
using TicketerAPI.DTOs;
using TicketerAPI.Models;

namespace TicketerAPI.ContTicketlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketRepo _repo;
        private readonly IMapper _mapper;

        public TicketsController(ITicketRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET /api/tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetAllTickets(string filter, int? filterId)
        {
            if(filter != null)
            {
                var tickets = await _repo.GetAll();
                var response = filter.ToLower() switch 
                {
                    "servicio" => tickets.Where(t => t.ServicioId == filterId),
                    "usuario" => tickets.Where(t => t.UsuarioId == filterId),
                    _ => tickets
                };
                return Ok(response);
            }
            return Ok(await _repo.GetAll());
        }

        // GET /api/tickets/1
        [HttpGet("{id}", Name = "GetTicketById")]
        public async Task<ActionResult<Ticket>> GetTicketById(int id)
        {
            var ticket = await _repo.GetById(id);

            if(ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }        

        // POST /api/tickets
        [HttpPost]
        public async Task<ActionResult<Ticket>> CreateTicket(TicketDTO ticketDto)
        {
            var ticket = _mapper.Map<Ticket>(ticketDto);
            await _repo.CreateEntity(ticket);
            await _repo.SaveChanges();

            return CreatedAtRoute(nameof(GetTicketById), new {id = ticket.Id}, ticket);
        }

        // PUT /api/tickets/1
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTicket(int id, TicketDTO ticketDto)
        {
            var ticketFromRepo = await _repo.GetById(id);

            if(ticketFromRepo == null)
                return NotFound();

            _mapper.Map(ticketDto, ticketFromRepo);

            _repo.UpdateEntity(ticketFromRepo); 

            await _repo.SaveChanges();

            return NoContent();
        }

        // PATCH /api/tickets/1 -> requiere JsonPatchDocument en el body
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdateTicket(int id, JsonPatchDocument<TicketDTO> patchDoc)
        {
            var ticketFromRepo = await _repo.GetById(id);

            if(ticketFromRepo == null)
                return NotFound();

            //var serviceToPatch = _mapper.Map<ServicioWriteDTO>(ticketFromRepo);
            var ticketToPatch = _mapper.Map<TicketDTO>(ticketFromRepo);
            patchDoc.ApplyTo(ticketToPatch, ModelState);   
            if(!TryValidateModel(ModelState)) 
            {
                return ValidationProblem(ModelState);
            }        
            
            _mapper.Map(ticketToPatch, ticketFromRepo);

            _repo.UpdateEntity(ticketFromRepo);

            await _repo.SaveChanges();

            return NoContent();
        }

        // DELETE /api/tickets/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTicket(int id)
        {
            var ticketFromRepo = await _repo.GetById(id);
            if(ticketFromRepo == null)
                return NotFound();

            _repo.DeleteEntity(ticketFromRepo);
            await _repo.SaveChanges();

            return NoContent();
        } 
        
    }
}