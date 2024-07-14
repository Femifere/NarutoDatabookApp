using AutoMapper;
using NarutoDatabookApp.Dto;
using NarutoDatabookApp.Models;

namespace NarutoDatabookApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Character, CharacterDto>();
            CreateMap<CharacterDto, Character>();

            CreateMap<Fan, FanDto>();
            CreateMap<FanDto, Fan>();

            CreateMap<Ranking, RankingDto>();
            CreateMap<RankingDto, Ranking>();

            CreateMap<Specialty, SpecialtyDto>();
            CreateMap<SpecialtyDto, Specialty>();

            CreateMap<Team, TeamDto>();
            CreateMap<TeamDto, Team>();

            CreateMap<Village, VillageDto>();
            CreateMap<VillageDto, Village>();

           
        }
    }
}
