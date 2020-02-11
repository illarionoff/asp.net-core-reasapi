﻿using System;
using System.Collections.Generic;
using RestAPI.Domain;

namespace RestAPI.Services
{
    public interface IPostService
    {
        List<Post> GetPosts();

        Post GetPostById(Guid postId);
    }
}