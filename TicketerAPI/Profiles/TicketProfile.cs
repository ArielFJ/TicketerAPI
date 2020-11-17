using AutoMapper;
using TicketerAPI.DTOs;
using TicketerAPI.Models;

namespace TicketerAPI.Profiles
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<TicketDTO, Ticket>();
            CreateMap<Ticket, TicketDTO>();
        }
    }
}