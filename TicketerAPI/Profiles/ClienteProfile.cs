using AutoMapper;
using TicketerAPI.DTOs;
using TicketerAPI.Models;

namespace TicketerAPI.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Cliente, ClienteDTO>();
            CreateMap<ClienteDTO, Cliente>();
        }
    }
}