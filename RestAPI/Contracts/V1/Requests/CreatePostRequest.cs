﻿using System;

namespace RestAPI.Contracts.V1.Requests
{
    public class CreatePostRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}