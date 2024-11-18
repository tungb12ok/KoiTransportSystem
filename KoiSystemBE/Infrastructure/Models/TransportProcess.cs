using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public partial class TransportProcess
    {
        public int ProcessId { get; set; }
        public int? OrderId { get; set; }
        public string Step { get; set; } = null!;
        public string? Status { get; set; }
        public DateTime? ProcessDate { get; set; }

        public virtual KoiOrder? Order { get; set; }
    }
}
