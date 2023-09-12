using System;
using API.Controllers;
using API.Dto;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Visma.Yuki.XUnitTest.Fixtures;

namespace Visma.Yuki.XUnitTest
{
	public class PostControllerTests: IClassFixture<PostControllerFixture>
	{

        private readonly PostControllerFixture _fixture;


        public PostControllerTests(PostControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetPostById_ExistingPost_Returns_OkResult()
        {
            // Arrange
            
            var postId = 1;
            var post = new Post { Id = postId };
            var mappedPost = new PostToListDto();

            _fixture.UnitOfWork.Setup(u => u.Repository<Post>().GetEntityWithSpec(It.IsAny<PostSpecification>()))
                .ReturnsAsync(post);
            _fixture.Mapper.Setup(m => m.Map<PostToListDto>(post)).Returns(mappedPost);

            

            // Act
            var result = await _fixture.postController.GetPostById(postId);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task SavePost_NonExistingAuthorAndPost_Returns_OkResult()
        {
            // Arrange

            var saveDto = new PostToAddDto();
            var authorId = 1;
            var postSpec = new PostSpecification(saveDto.Title, saveDto.Description, authorId);

            _fixture.UnitOfWork.Setup(u => u.Repository<Author>().GetByIdAsync(authorId))
                .ReturnsAsync((Author)null);

            _fixture.UnitOfWork.Setup(u => u.Repository<Post>().GetEntityWithSpec(postSpec))
                .ReturnsAsync((Post)null);

            _fixture.Mapper.Setup(m => m.Map<Post>(saveDto)).Returns(new Post());

            

            // Act
            var result = await _fixture.postController.SavePost(saveDto);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task SavePost_ExistingAuthor_Returns_BadRequestResult()
        {
            // Arrange

            var saveDto = new PostToAddDto();
            var authorId = 1;
            var authorSpec = new AuthorSpecification(authorId);

            _fixture.UnitOfWork.Setup(u => u.Repository<Author>().GetEntityWithSpec(authorSpec))
                .ReturnsAsync(new Author());

            // Act
            var result = await _fixture.postController.SavePost(saveDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task SavePost_ExistingPost_Returns_BadRequestResult()
        {
            // Arrange

            var saveDto = new PostToAddDto();
            var authorId = 1;
            var authorSpec = new AuthorSpecification(authorId);
            var postSpec = new PostSpecification(saveDto.Title, saveDto.Description, authorId);

            _fixture.UnitOfWork.Setup(u => u.Repository<Author>().GetEntityWithSpec(authorSpec))
                .ReturnsAsync((Author)null);

            _fixture.UnitOfWork.Setup(u => u.Repository<Post>().GetEntityWithSpec(postSpec))
                .ReturnsAsync(new Post());

            // Act
            var result = await _fixture.postController.SavePost(saveDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}

