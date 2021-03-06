using CommunityProject.Models;
using CommunityProject.Models.Services;
using CommunityProject.Models.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CommunityProject.Controllers
{
  
    [Route("api/posts")]
    [ApiController]
   
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }
       
        [HttpGet]
        public IEnumerable<Post> Get()
        {
            
            IEnumerable<Post> list = _postService.GetAll();
           

            return list;
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public Post GetPost(int id)
        {
            Post post = _postService.FindById(id);
          
           
            return post;
            
        }

      
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public void Post([FromBody] CreatePostViewModel createPost)
        {
            
           Post post = _postService.Create(createPost);
            if (post != null)
            {
                Response.StatusCode = 201;
            }
            else
            {
                Response.StatusCode = 400;
            }
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CreatePostViewModel editPost)
        {
            _postService.Edit(id, editPost);
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _postService.Remove(id);
        }
    }
}
