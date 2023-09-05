using System;
using API.Controllers;
using AutoMapper;
using Core.Interfaces;
using Moq;

namespace Visma.Yuki.XUnitTest.Fixtures
{
	public class AuthorControllerFixture
	{
        public Mock<IUnitOfWork> UnitOfWork { get; }
        public Mock<IMapper> Mapper { get; }
        public AuthorController authorController { get; }

        public AuthorControllerFixture()
        {
            UnitOfWork = new Mock<IUnitOfWork>();
            Mapper = new Mock<IMapper>();
            authorController = new AuthorController(UnitOfWork.Object, Mapper.Object);
        }

    }
}

