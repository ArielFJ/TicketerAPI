using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketerAPI.Data;
using TicketerAPI.Models;
using TicketerAPI.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace TicketerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly IServicioRepo _repo;
        private readonly IMapper _mapper;

        public ServiciosController(IServicioRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET /api/servicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servicio>>> GetAllServicios()
        {
            return Ok(await _repo.GetAll());
        }

        // GET /api/servicios/1
        [HttpGet("{id}", Name = "GetServicioById")]
        public async Task<ActionResult<Servicio>> GetServicioById(int id)
        {
            var servicio = await _repo.GetById(id);

            if(servicio == null)
            {
                return NotFound();
            }
            return Ok(servicio);
        }

        // POST /api/servicios
        [HttpPost]
        public async Task<ActionResult<Servicio>> CreateServicio(ServicioWriteDTO servicioDto)
        {
            var servicio = _mapper.Map<Servicio>(servicioDto);
            await _repo.CreateEntity(servicio);
            await _repo.SaveChanges();

            return CreatedAtRoute(nameof(GetServicioById), new {id = servicio.Id}, servicio);
        }

        // PUT /api/servicios/1
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateServicio(int id, ServicioWriteDTO servicioDto)
        {
            var servicioFromRepo = await _repo.GetById(id);

            if(servicioFromRepo == null)
                return NotFound();

            _mapper.Map(servicioDto, servicioFromRepo);

            _repo.UpdateEntity(servicioFromRepo); 

            await _repo.SaveChanges();

            return NoContent();
        }

        // PATCH /api/servicios/1 -> requiere JsonPatchDocument en el body
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdateServicio(int id, JsonPatchDocument<Servicio> patchDoc)
        {
            var servicioFromRepo = await _repo.GetById(id);

            if(servicioFromRepo == null)
                return NotFound();
            
            patchDoc.ApplyTo(servicioFromRepo, ModelState);   
            if(!TryValidateModel(ModelState)) 
            {
                return ValidationProblem(ModelState);
            }        

            _repo.UpdateEntity(servicioFromRepo);

            await _repo.SaveChanges();

            return NoContent();
        }

        // DELETE /api/servicios/1        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteServicio(int id)
        {
            var servicioFromRepo = await _repo.GetById(id);
            if(servicioFromRepo == null)
                return NotFound();

            _repo.DeleteEntity(servicioFromRepo);
            await _repo.SaveChanges();

            return NoContent();
        }
    }
}