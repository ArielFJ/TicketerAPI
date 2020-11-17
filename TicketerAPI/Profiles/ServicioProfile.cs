using AutoMapper;
using TicketerAPI.DTOs;
using TicketerAPI.Models;

namespace TicketerAPI.Profiles
{
    public class ServicioProfile : Profile
    {
        public ServicioProfile()
        {
            CreateMap<ServicioWriteDTO, Servicio>();
            CreateMap<Servicio, ServicioWriteDTO>();

        }
    }
}