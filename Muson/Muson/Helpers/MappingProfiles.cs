using AutoMapper;
using Domain.Models;
using Domain.Models.ViewModels;

namespace Muson.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {
            CreateMap<Room, RoomViewModel>().ReverseMap();
            CreateMap<Booking, BookingViewModel>().ReverseMap();
        }
    }
}
