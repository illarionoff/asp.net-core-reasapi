using System;
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
        public IActionResult GetAll()
        {
            return Ok(_postService.GetPosts());
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public IActionResult Get(Guid postId)
        {
            var post = _postService.GetPostById(postId);

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post
            {
                Id = postRequest.Id,
                Name = postRequest.Name
            };

            if (post.Id != Guid.Empty) post.Id = Guid.NewGuid();

            _postService.GetPosts().Add(post);

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