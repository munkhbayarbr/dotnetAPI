using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetAPI.Dtos.Stock;
using dotnetAPI.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Dtos.Stock;
using MyApi.Mappers;

namespace MyApi.Controllers
{
   [Route("api/comment")]
   [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        public CommentController(ICommentRepository repository)
        {
            _commentRepository = repository;
        }
        [HttpGet]
       
       public async Task<IActionResult> GetAll()
       {
           var comments = await _commentRepository.GetAllAsync();
           var commentDto = comments.Select(x=>x.ToCommentDto());
           return Ok(commentDto);
       }
       [HttpGet("{id}")]
         public async Task<IActionResult> GetById([FromRoute] int id)
         {
              var comment = await _commentRepository.GetByIdAsync(id);
              if(comment == null)
              {
                return NotFound();
              }
              return Ok(comment.ToCommentDto());
         }
    }
}