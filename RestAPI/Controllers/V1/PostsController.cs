using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts.V1;
using RestAPI.Contracts.V1.Requests;
using RestAPI.Contracts.V1.Responses;
using RestAPI.Domain;
using RestAPI.Services;

namespace RestAPI.Controllers.V1
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _postService.GetPostsAsync());
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get(Guid postId)
        {
            var post =await _postService.GetPostByIdAsync(postId);

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update(Guid postId, [FromBody] UpdatePostRequest request)
        {
            var post = new Post
            {
                Id = postId,
                Name = request.Name
            };

            var updated = await _postService.UpdatePostAsync(post);

            if (!updated)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete(Guid postId)
        {
            var removed = await _postService.DeletePostAsync(postId);

            if (!removed)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post
            {
                Name = postRequest.Name
            };
            
            await _postService.CreatePostAsync(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString());

            var response = new PostResponse
            {
                Id = post.Id,
                Name = post.Name
            };

            return Created(locationUrl, response);
        }
    }
}