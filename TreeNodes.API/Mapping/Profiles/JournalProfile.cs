using AutoMapper;
using TreeNodes.API.Models;
using TreeNodes.API.Models.DTOs;
using TreeNodes.Data.Models;

namespace TreeNodes.API.Mapping.Profiles
{
    public class JournalProfile : Profile
    {
        public JournalProfile()
        {
            CreateMap<Journal, JournalRecord>()
                .ForMember(x => x.Id, cfg => cfg.MapFrom(x => x.Id))
                .ForMember(x => x.Type, cfg => cfg.MapFrom(x => x.Type))
                .ForMember(x => x.Data, cfg => cfg.MapFrom(x => new TreeExceptionData { Message = x.Data }))
                .ForMember(x => x.CreatedAt, cfg => cfg.MapFrom(x => x.CreatedAt));

            CreateMap<JournalRecord, Journal>()
                .ForMember(x => x.Data, cfg => cfg.MapFrom(x => x.Data.Message))
                .ForMember(x => x.Type, cfg => cfg.MapFrom(x => x.Type));
        }
    }
}
