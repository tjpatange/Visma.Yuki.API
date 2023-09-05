using System;
using API.Dto;
using AutoMapper;
using Core.Entities;

namespace API.MappingProfiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorToListDto>()
          .ForMember(d => d.FullName, o => o.MapFrom(s => s.Name + " " + s.SurName))
          .ReverseMap();
            CreateMap<Author, AuthorToAddDto>().ReverseMap();
        }
    }
}

