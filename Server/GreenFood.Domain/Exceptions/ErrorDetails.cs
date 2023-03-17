﻿using System.Text.Json;

namespace GreenFood.Domain.Exceptions
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
