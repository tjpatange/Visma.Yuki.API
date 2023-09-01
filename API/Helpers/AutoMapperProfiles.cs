using System;
using API.Dto;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
		public AutoMapperProfiles()
		{
            #region Author
            CreateMap<Author, AuthorToListDto>()
           .ForMember(d => d.FullName, o => o.MapFrom(s => s.Name + " " + s.SurName))
           .ReverseMap();
            CreateMap<Author, AuthorToAddDto>().ReverseMap();
            #endregion

            #region Post

            CreateMap<Post, PostToListDto>().ReverseMap();
            CreateMap<Author, PostToAddDto>().ReverseMap();

            #endregion

        }
    }
}

