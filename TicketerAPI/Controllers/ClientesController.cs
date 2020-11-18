using System.Collections.Generic;
using System.Linq;
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
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepo _repo;
        private readonly IMapper _mapper;

        public ClientesController(IClienteRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET /api/clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAllClientes(string nombre)
        {
            var clienteItems = await _repo.GetAll();

            if(nombre == null)
                return Ok(clienteItems);

            var response = clienteItems.Where(c => c.Nombre.ToLower().Contains(nombre.ToLower()));

            return Ok(response);
        }

        // GET /api/clientes/1
        [HttpGet("{id}", Name = "GetClienteById")]
        public async Task<ActionResult<Cliente>> GetClienteById(int id)
        {
            var cliente = await _repo.GetById(id);

            if(cliente != null)
                return Ok(cliente);

            return NotFound();
        }

        // POST /api/clientes
        [HttpPost]
        public async Task<ActionResult<Cliente>> CreateUsuario(ClienteDTO usuarioCreateDTO)
        {            
            var cliente = _mapper.Map<Cliente>(usuarioCreateDTO);
            await _repo.CreateEntity(cliente);
            await _repo.SaveChanges();            

            return CreatedAtRoute(nameof(GetClienteById), new {Id = cliente.Id}, cliente);
            // return Ok(usuarioReadDto);
        }

        // PUT /api/clientes/1
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCliente(int id, ClienteDTO clienteDTO) 
        {
            var clienteFromRepo = await _repo.GetById(id);
            if(clienteFromRepo == null)  
                return NotFound();

            _mapper.Map(clienteDTO, clienteFromRepo);

            _repo.UpdateEntity(clienteFromRepo);

            await _repo.SaveChanges();

            return NoContent();
        }

        // PATCH /api/clientes/1 -> requiere JsonPatchDocument en el body
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdateUsuario(int id, JsonPatchDocument<ClienteDTO> patchDoc)
        {
            var clienteFromRepo = await _repo.GetById(id);
            if(clienteFromRepo == null)  
                return NotFound();

            var clienteToPatch = _mapper.Map<ClienteDTO>(clienteFromRepo);
            patchDoc.ApplyTo(clienteToPatch, ModelState);
            if(!TryValidateModel(clienteToPatch)) 
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(clienteToPatch, clienteFromRepo);

            _repo.UpdateEntity(clienteFromRepo);

            await _repo.SaveChanges();

            return NoContent();
        }

        // DELETE /api/clientes/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCliente(int id)
        {
            var clienteFromRepo = await _repo.GetById(id);
            if(clienteFromRepo == null)
                return NotFound();

            _repo.DeleteEntity(clienteFromRepo);
            await _repo.SaveChanges();

            return NoContent();
        }
    }
}