using System;
using API.Dto;
using API.MappingProfiles;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Moq;
using Visma.Yuki.XUnitTest.Fixtures;

namespace Visma.Yuki.XUnitTest
{
	public class GenericRepositoryTestWithoutStoreContext: IClassFixture<AuthorGeneralRepositoryFixture>
	{
        private readonly AuthorGeneralRepositoryFixture _fixture;


        public GenericRepositoryTestWithoutStoreContext(AuthorGeneralRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
		public async void GetByIdAsync_ReturnsEntity_IfExists()
		{
            // Arrange

            var authorEntityToAdd = new AuthorToAddDto("TJ", "Patange");
            var author = _fixture.Mapper.Map<Author>(authorEntityToAdd);

            _fixture.AuthorRepositoryMock
             .Setup(m => m.GetByIdAsync(It.IsAny<int>()))
             .ReturnsAsync(author);


            // Act

            var executeGetById = await _fixture.AuthorRepositoryMock.Object.GetByIdAsync(1);


            // Assert

            Assert.NotNull(executeGetById);
            Assert.Equal(author.Name, executeGetById.Name);
            Assert.Equal(author.SurName, executeGetById.SurName);

        }


        [Fact]
        public async void GetByIdAsync_ReturnsNull_IfEntityDoesNoyExists()
        {
            // Arrange

            _fixture.AuthorRepositoryMock
             .Setup(m => m.GetByIdAsync(It.IsAny<int>()))
             .ReturnsAsync((Author)null);


            // Act

            var executeGetById = await _fixture.AuthorRepositoryMock.Object.GetByIdAsync(1);


            // Assert

            Assert.Null(executeGetById);

        }


        [Fact]
        public async void GetAllAsync_ReturnsAllEntities()
        {
            // Arrange

            var authorEntitiesToAdd = new List<AuthorToAddDto>
            {
                new AuthorToAddDto { Name = "TJ", SurName = "Patange" },
                new AuthorToAddDto { Name = "Trinay", SurName = "Sharma" },
                new AuthorToAddDto { Name = "Yuki", SurName = "Visma" }
            };

            var authors = _fixture.Mapper.Map<List<Author>>(authorEntitiesToAdd);

            _fixture.AuthorRepositoryMock
             .Setup(m => m.GetAllAsync())
             .ReturnsAsync(authors);


            // Act

            var executeGetAll = await _fixture.AuthorRepositoryMock.Object.GetAllAsync();


            // Assert

            Assert.NotNull(executeGetAll);
            Assert.Equal(authorEntitiesToAdd.Count, executeGetAll.Count());

        }

        [Fact]
        public void Add_ReturnsEntity_IfAdded()
        {
            // Arrange

            var authorEntityToAdd = new AuthorToAddDto("TJ", "Patange");
            var author = _fixture.Mapper.Map<Author>(authorEntityToAdd);


            // Act

            _fixture.AuthorRepositoryMock.Object.Add(author);


            // Assert

            _fixture.AuthorRepositoryMock.Verify(r => r.Add(It.IsAny<Author>()), Times.Once);
            _fixture.AuthorRepositoryMock.Verify(r => r.Add(author), Times.Once);

        }


        [Fact]
        public void Update_Entity()
        {
            // Arrange

            var entityToUpdate = new AuthorToAddDto("TJ", "Patange");

            var entityUpdated = new AuthorToAddDto("TJ", "Sharma");

            var author = _fixture.Mapper.Map<Author>(entityToUpdate);
            var authorUpdated = _fixture.Mapper.Map<Author>(entityUpdated);

            _fixture.AuthorRepositoryMock
                .Setup(r => r.Update(It.IsAny<Author>())).Callback<Author>(entity =>
                {
                    author.SurName = entity.SurName;
                });


            // Act

            _fixture.AuthorRepositoryMock.Object.Update(authorUpdated);


            // Assert

            _fixture.AuthorRepositoryMock.Verify(r => r.Update(It.IsAny<Author>()), Times.Once);
            Assert.Equal(entityUpdated.Name, author.Name);

        }

        [Fact]
        public void Remove_Entity()
        {
            // Arrange

            var entityToDelete = new AuthorToAddDto("TJ", "Patange");
            var author = _fixture.Mapper.Map<Author>(entityToDelete);

            _fixture.AuthorRepositoryMock
                .Setup(r => r.Remove(It.IsAny<Author>())).Callback<Author>(entity =>
                {
                    
                });


            // Act

            _fixture.AuthorRepositoryMock.Object.Remove(author);


            // Assert

            _fixture.AuthorRepositoryMock.Verify(r => r.Remove(It.IsAny<Author>()), Times.Once);

        }

    }
}

