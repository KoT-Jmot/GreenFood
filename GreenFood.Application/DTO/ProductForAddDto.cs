﻿namespace GreenFood.Application.DTO
{
    public class ProductForAddDto
    {
        public string Header { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Count { get; set; } 
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}