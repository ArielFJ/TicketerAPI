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
    public class RolesController : ControllerBase
    {
        private readonly IRolRepo _repo;
        private readonly IMapper _mapper;

        public RolesController(IRolRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET /api/roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rol>>> GetAllRoles()
        {
            return Ok(await _repo.GetAll());
        }

        // GET /api/roles/1
        [HttpGet("{id}", Name = "GetRolById")]
        public async Task<ActionResult<Rol>> GetRolById(int id)
        {
            var rol = await _repo.GetById(id);

            if(rol == null)
            {
                return NotFound();
            }
            return Ok(rol);
        }

        // POST /api/roles
        [HttpPost]
        public async Task<ActionResult<Rol>> CreateRol(RolWriteDTO rolDto)
        {
            var rol = _mapper.Map<Rol>(rolDto);
            await _repo.CreateEntity(rol);
            await _repo.SaveChanges();

            return CreatedAtRoute(nameof(GetRolById), new {id = rol.Id}, rol);
        }

        // PUT /api/roles/1
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRol(int id, RolWriteDTO rolDto)
        {
            var rolFromRepo = await _repo.GetById(id);

            if(rolFromRepo == null)
                return NotFound();

            _mapper.Map(rolDto, rolFromRepo);

            _repo.UpdateEntity(rolFromRepo); 

            await _repo.SaveChanges();

            return NoContent();
        }

        // PATCH /api/roles/1 -> requiere JsonPatchDocument en el body
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdateRol(int id, JsonPatchDocument<RolWriteDTO> patchDoc)
        {
            var rolFromRepo = await _repo.GetById(id);

            if(rolFromRepo == null)
                return NotFound();
        
            var rolToPatch = _mapper.Map<RolWriteDTO>(rolFromRepo);
            patchDoc.ApplyTo(rolToPatch, ModelState);   
            if(!TryValidateModel(ModelState)) 
            {
                return ValidationProblem(ModelState);
            }        
            
            _mapper.Map(rolToPatch, rolFromRepo);

            _repo.UpdateEntity(rolFromRepo);

            await _repo.SaveChanges();

            return NoContent();
        }

        // DELETE /api/roles/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRol(int id)
        {
            var rolFromRepo = await _repo.GetById(id);
            if(rolFromRepo == null)
                return NotFound();

            _repo.DeleteEntity(rolFromRepo);
            await _repo.SaveChanges();

            return NoContent();
        }        
    }
}