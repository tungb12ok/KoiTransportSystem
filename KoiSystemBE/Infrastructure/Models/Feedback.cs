using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public partial class Feedback
    {
        public int FeedbackId { get; set; }
        public int? OrderId { get; set; }
        public int? CustomerId { get; set; }
        public int Rating { get; set; }
        public string? Comments { get; set; }
        public DateTime? FeedbackDate { get; set; }

        public virtual User? Customer { get; set; }
        public virtual KoiOrder? Order { get; set; }
    }
}
