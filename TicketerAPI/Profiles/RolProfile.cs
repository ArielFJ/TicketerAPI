using AutoMapper;
using TicketerAPI.DTOs;
using TicketerAPI.Models;

namespace TicketerAPI.Profiles
{
    public class RolProfile : Profile
    {
        public RolProfile()
        {
            CreateMap<RolWriteDTO, Rol>();
            CreateMap<Rol, RolWriteDTO>();
        }
    }
}