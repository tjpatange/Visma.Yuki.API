using System;
using API.Dto;
using AutoMapper;
using Core.Entities;

namespace API.MappingProfiles
{
	public class PostProfile: Profile
	{
		public PostProfile()
		{
            CreateMap<Post, PostToListDto>().ReverseMap();
            CreateMap<Post, PostToAddDto>().ReverseMap();
        }
	}
}

