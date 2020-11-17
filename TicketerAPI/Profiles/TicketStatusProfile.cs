using AutoMapper;
using TicketerAPI.DTOs;
using TicketerAPI.Models;

namespace TicketerAPI.Profiles
{
    public class TicketStatusProfile : Profile
    {
        public TicketStatusProfile()
        {
            CreateMap<TicketStatusDTO, TicketStatus>();
            CreateMap<TicketStatus, TicketStatusDTO>();

            CreateMap<TicketStatusDTO, TicketPrioridad>();
            CreateMap<TicketPrioridad, TicketStatusDTO>();
        }
    }
}