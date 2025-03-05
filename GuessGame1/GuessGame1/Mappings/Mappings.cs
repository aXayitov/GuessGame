using AutoMapper;
using GuessGame1.DTOs.GameDTO;
using GuessGame1.DTOs.UserDTO;
using GuessGame1.Entity;

namespace GuessGame1.Mappings
{
    public class Mappings : Profile
    {
        public Mappings() 
        {
            CreateMap<User, UserDto>()
            .ForMember(dest => dest.Games, opt => opt.MapFrom(src => src.Games));

            CreateMap<Game, GameDto>();
        } 
    }
}
