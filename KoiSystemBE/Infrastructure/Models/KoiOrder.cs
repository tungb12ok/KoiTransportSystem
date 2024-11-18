using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public partial class KoiOrder
    {
        public KoiOrder()
        {
            Feedbacks = new HashSet<Feedback>();
            OrderDocuments = new HashSet<OrderDocument>();
            TransportProcesses = new HashSet<TransportProcess>();
        }

        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public string PickupLocation { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public decimal Weight { get; set; }
        public int Quantity { get; set; }
        public string TransportMethod { get; set; } = null!;
        public decimal? Total { get; set; }
        public string? AdditionalServices { get; set; }
        public int? PricingId { get; set; }
        public string? Status { get; set; }
        public DateTime? PlacedDate { get; set; }
        public DateTime? CompletedDate { get; set; }

        public virtual User? Customer { get; set; }
        public virtual Pricing? Pricing { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<OrderDocument> OrderDocuments { get; set; }
        public virtual ICollection<TransportProcess> TransportProcesses { get; set; }
    }
}
