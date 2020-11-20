using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TicketerAPI.Models;
using TicketerAPI.Data;
using AutoMapper;
using TicketerAPI.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using System.Threading.Tasks;

namespace TicketerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepo _userRepo;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        // GET /api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAllUsers()
        {
            var userItems = await _userRepo.GetAll();

            return Ok(_mapper.Map<IEnumerable<UsuarioReadDTO>>(userItems));
        }

        // GET /api/usuarios/1
        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<ActionResult<Usuario>> GetUserById(int id)
        {
            var userItem = await _userRepo.GetById(id);

            if(userItem != null)
                return Ok(_mapper.Map<UsuarioReadDTO>(userItem));

            return NotFound();
        }

        // GET /api/usuarios/check
        [HttpGet("check")]
        public ActionResult<Usuario> CheckUser(LoginDTO loginDTO)
        {            
            Usuario usuario = new Usuario();
            var userExists = _userRepo.CheckUser(loginDTO.NombreUsuario, loginDTO.Contrasena, out usuario);

            if(usuario != null)
            {
                if(!userExists)
                {
                    ModelState.AddModelError("No authenticated", "Your username or password seems incorrect");
                    return Ok(ModelState);
                }
                                
                return Ok(usuario);
            }

            return NotFound();
        }

        // POST /api/usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> CreateUsuario(UsuarioCreateDTO usuarioCreateDTO)
        {            
            var usuario = _mapper.Map<Usuario>(usuarioCreateDTO);
            await _userRepo.CreateEntity(usuario);
            await _userRepo.SaveChanges();            
            
            usuario = await _userRepo.GetById(usuario.Id);

            return CreatedAtRoute(nameof(GetUserById), new {Id = usuario.Id}, usuario);
            // return Ok(usuarioReadDto);
        }

        // PUT /api/usuarios/1
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUsuario(int id, UsuarioCreateDTO usuarioCreateDTO) 
        {
            var usuarioFromRepo = await _userRepo.GetById(id);
            if(usuarioFromRepo == null)  
                return NotFound();

            _mapper.Map(usuarioCreateDTO, usuarioFromRepo);

            _userRepo.UpdateEntity(usuarioFromRepo);

            await _userRepo.SaveChanges();

            return NoContent();
        }

        // PATCH /api/usuarios/1 -> requiere JsonPatchDocument en el body
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdateUsuario(int id, JsonPatchDocument<UsuarioCreateDTO> patchDoc)
        {
            var usuarioFromRepo = await _userRepo.GetById(id);
            if(usuarioFromRepo == null)  
                return NotFound();

            var usuarioToPatch = _mapper.Map<UsuarioCreateDTO>(usuarioFromRepo);
            patchDoc.ApplyTo(usuarioToPatch, ModelState);
            if(!TryValidateModel(usuarioToPatch)) 
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(usuarioToPatch, usuarioFromRepo);

            _userRepo.UpdateEntity(usuarioFromRepo);

            await _userRepo.SaveChanges();

            return NoContent();
        }

        // DELETE /api/usuarios/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUsuario(int id)
        {
            var usuarioFromRepo = await _userRepo.GetById(id);
            if(usuarioFromRepo == null)
                return NotFound();

            _userRepo.DeleteEntity(usuarioFromRepo);
            await _userRepo.SaveChanges();

            return NoContent();
        }
    } 
}