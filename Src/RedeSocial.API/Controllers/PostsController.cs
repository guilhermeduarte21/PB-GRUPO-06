using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Post;
using RedeSocial.Services.Post;

namespace RedeSocial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            this._postService = postService;
        }

        // GET: api/<PostsController>
        [HttpGet]
        public async Task<IEnumerable<Postagem>> Get()
        {
            return await _postService.GetPostsAsync();
        }

        // GET api/<PostsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Postagem>> Get(Guid id)
        {
            var post = await _postService.FindByIdAsync(id.ToString(), default); ;

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // POST api/<PostsController>
        [HttpPost]
        public async Task<ActionResult<Postagem>> Post(Postagem postagem)
        {
            await _postService.CreateAsync(postagem, default);

            return CreatedAtAction("GetPost", new { id = postagem.ID });
        }

        // PUT api/<PostsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Postagem postagem)
        {
            if (id != postagem.ID)
            {
                return BadRequest();
            }


            try
            {
                await _postService.UpdateAsync(postagem, default);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<PostsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Postagem>> DeleteAccount(Guid id)
        {
            var post = await _postService.FindByIdAsync(id.ToString(), default);
            if (post == null)
            {
                return NotFound();
            }

            await _postService.DeleteAsync(post, default);

            return post;
        }

        private bool PostExists(Guid id)
        {
            return _postService.FindByIdAsync(id.ToString(), default) != null;
        }
    }
}
