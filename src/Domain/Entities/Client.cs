﻿using Domain.Common;

namespace Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PostCode { get; set; }
    }
}
