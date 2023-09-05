using System;
using API.MappingProfiles;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Moq;

namespace Visma.Yuki.XUnitTest.Fixtures
{
	public class AuthorGeneralRepositoryFixture: IDisposable
	{
        public Mock<IGenericRepository<Author>> AuthorRepositoryMock{ get; }
        public IMapper Mapper { get; private set; }
        public MapperConfiguration MapperConfiguration { get; private set; }

        public AuthorGeneralRepositoryFixture()
		{
            AuthorRepositoryMock = new Mock<IGenericRepository<Author>>();
            MapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<AuthorProfile>());
            Mapper = new Mapper(MapperConfiguration);
        }

        public void Dispose()
        {
        }
    }
}

