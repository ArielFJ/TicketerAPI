using AutoMapper;
using TicketerAPI.DTOs;
using TicketerAPI.Models;

namespace TicketerAPI.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            // CreateMap<Usuario, UsuarioReadDTO>()
            //     .ForMember(usuarioDto => usuarioDto.Rol,
            //         opt => opt.MapFrom(usuario => usuario.Rol.Nombre));
            CreateMap<UsuarioCreateDTO, Usuario>();
            CreateMap<Usuario, UsuarioCreateDTO>();

            CreateMap<Usuario, UsuarioReadDTO>();
        }
    }
}