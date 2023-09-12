using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using API.Dto;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PostController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PostController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostToListDto>> GetPostById(int id)
        {
            var spec = new PostSpecification(id);
            var post = await _unitOfWork.Repository<Post>().GetEntityWithSpec(spec);
            var postFromMapper = _mapper.Map<PostToListDto>(post);
            return Ok(postFromMapper);
        }


        [HttpPost]
        public async Task<ActionResult> SavePost(PostToAddDto saveDto)
        {
            var existingAuthor = await _unitOfWork.Repository<Author>().GetByIdAsync(saveDto.AuthorId);
            if (existingAuthor == null)
                return BadRequest("You are trying to add post with an non-existing Author!!");

            var spec = new PostSpecification(saveDto.Title, saveDto.Description, saveDto.AuthorId);
            var existingPost = await _unitOfWork.Repository<Post>().GetEntityWithSpec(spec);
            if (existingPost != null)
                return BadRequest("Already exists");

            var postFromMapper = _mapper.Map<PostToAddDto, Post>(saveDto);
            _unitOfWork.Repository<Post>().Add(postFromMapper);
            int isCreated = await _unitOfWork.Complete();

            if (isCreated == 0)
            {
                return BadRequest("Error while saving post");
            }

            return Ok("Saved!");
        }
    }
}
