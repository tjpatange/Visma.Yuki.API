using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dto;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthorController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet()]
        [TypeFilter(typeof(ResponseFormatFilter))]
        public async Task<ActionResult<IReadOnlyList<AuthorToListDto>>> GetAllAuthors()
        {
            var authors = await _unitOfWork.Repository<Author>().GetAllAsync();
            var authorsFromMapper = _mapper.Map<IReadOnlyList<AuthorToListDto>>(authors);
            return Ok(authorsFromMapper);
        }


        [HttpGet("{id}")]
        [TypeFilter(typeof(ResponseFormatFilter))]
        public async Task<ActionResult<AuthorToListDto>> GetPostById(int id)
        {
            var spec = new AuthorSpecification(id);
            var author = await _unitOfWork.Repository<Author>().GetEntityWithSpec(spec);
            var authorFromMapper = _mapper.Map<PostToListDto>(author);
            return Ok(authorFromMapper);
        }

       
        [HttpPost]
        public async Task<ActionResult> SaveAuthor(AuthorToAddDto saveDto)
        {
            var spec = new AuthorSpecification(saveDto.Name, saveDto.SurName);
            var existingAuthor = await _unitOfWork.Repository<Author>().GetEntityWithSpec(spec);
            if (existingAuthor != null)
                return BadRequest("Already exists");

            var authorFromMapper = _mapper.Map<AuthorToAddDto, Author>(saveDto);
            _unitOfWork.Repository<Author>().Add(authorFromMapper);
            int isCreated = await _unitOfWork.Complete();

            if (isCreated == 0)
            {
                return BadRequest("Error while saving author");
            }

            return Ok("Saved!");
        }


    }
}
