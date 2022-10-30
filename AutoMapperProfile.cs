using AutoMapper;
using FootballProgrammes.Dtos.Programmes;
using FootballProgrammes.Models;

namespace FootballProgrammes
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<FootballProgramme, GetProgrammeDto>();
        }
    }
}
