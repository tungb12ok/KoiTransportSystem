using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public partial class Customer
    {
        public int CustomerId { get; set; }
        public string? ProfileInfo { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
