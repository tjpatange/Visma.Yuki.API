using System;
using API.Controllers;
using API.MappingProfiles;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Moq;

namespace Visma.Yuki.XUnitTest.Fixtures
{
	public class PostControllerFixture : IDisposable
	{
        public Mock<IUnitOfWork> UnitOfWork { get; }
        public Mock<IMapper> Mapper { get;  }
        public PostController postController { get; }

        public PostControllerFixture()
        {
            UnitOfWork = new Mock<IUnitOfWork>();
            Mapper = new Mock<IMapper>();
            postController = new PostController(UnitOfWork.Object, Mapper.Object);
        }

        public void Dispose()
        {
        }
    }
}

