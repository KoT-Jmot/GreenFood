﻿namespace GreenFood.Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
        public Guid UserId { get; set; }
        public virtual ApplicationUser Customer { get; set; } = null!;
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
        public DateTime CreateDate { get; set; }
    }
}
