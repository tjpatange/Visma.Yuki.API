using System;
using API.Controllers;
using API.Dto;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Visma.Yuki.XUnitTest.Fixtures;

namespace Visma.Yuki.XUnitTest
{
	public class AuthorControllerTests: IClassFixture<AuthorControllerFixture>
	{
        private readonly AuthorControllerFixture _fixture;


        public AuthorControllerTests(AuthorControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetAllAuthors_Returns_OkResult()
        {
            // Arrange

            var authors = new List<Author>();
            var mappedAuthors = new List<AuthorToListDto>();

            _fixture.UnitOfWork.Setup(u => u.Repository<Author>().GetAllAsync()).ReturnsAsync(authors);
            _fixture.Mapper.Setup(m => m.Map<IReadOnlyList<AuthorToListDto>>(authors)).Returns(mappedAuthors);

            // Act
            var result = await _fixture.authorController.GetAllAuthors();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAuthorById_ExistingAuthor_Returns_OkResult()
        {
            // Arrange

            var authorId = 1;
            var author = new Author { Id = authorId };
            var mappedAuthor = new AuthorToListDto();

            _fixture.UnitOfWork.Setup(u => u.Repository<Author>().GetEntityWithSpec(It.IsAny<AuthorSpecification>()))
                .ReturnsAsync(author);
            _fixture.Mapper.Setup(m => m.Map<AuthorToListDto>(author)).Returns(mappedAuthor);

            // Act
            var result = await _fixture.authorController.GetPostById(authorId);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task SaveAuthor_NonExistingAuthor_Returns_OkResult()
        {
            // Arrange
            //var connection = new SqliteConnection("Data Source=:memory:");
            //connection.Open();
//
            //var optionsBuilder = new DbContextOptionsBuilder<StoreContext>()
            //          .UseSqlite(connection);
//
            //var dbContext = new StoreContext(optionsBuilder.Options);
            //var loggerFactorMock = new Mock<ILoggerFactory>();
            //dbContext.Database.Migrate();
            //await StoreContextSeed.SeedAsync(dbContext, loggerFactorMock.Object);
//
            var saveDto = new AuthorToAddDto { Name = "TJ", SurName = "Patange"};
            var spec = new AuthorSpecification(saveDto.SurName, saveDto.SurName);

            _fixture.UnitOfWork.Setup(u => u.Repository<Author>().GetEntityWithSpec(spec))
                .ReturnsAsync((Author)null);
            _fixture.Mapper.Setup(m => m.Map<Author>(saveDto)).Returns(new Author());

            // Act
            var result = await _fixture.authorController.SaveAuthor(saveDto);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task SaveAuthor_ExistingAuthor_Returns_BadRequestResult()
        {
            // Arrange

            var saveDto = new AuthorToAddDto();
            var spec = new AuthorSpecification(saveDto.SurName, saveDto.SurName);

            _fixture.UnitOfWork.Setup(u => u.Repository<Author>().GetEntityWithSpec(spec))
                .ReturnsAsync(new Author());


            // Act
            var result = await _fixture.authorController.SaveAuthor(saveDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}

