using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public partial class OrderDocument
    {
        public int DocumentId { get; set; }
        public int? OrderId { get; set; }
        public string DocumentType { get; set; } = null!;
        public string DocumentPath { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public string? Image { get; set; }

        public virtual KoiOrder? Order { get; set; }
    }
}
