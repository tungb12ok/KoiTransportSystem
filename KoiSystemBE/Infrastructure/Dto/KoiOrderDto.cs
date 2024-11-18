using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace ApplicationCore.Dto
{
    public class KoiOrderDto
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PickupLocation { get; set; }
        public string Destination { get; set; }
        public decimal Weight { get; set; }
        public int Quantity { get; set; }
        public string TransportMethod { get; set; }
        public decimal? Total { get; set; }
        public string? AdditionalServices { get; set; }
        public int? PricingId { get; set; }
        public string PricingDetails { get; set; } 
        public string? Status { get; set; }
        public DateTime? PlacedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
    }

    public class CreateKoiOrderDto
    {
        public int CustomerId { get; set; }
        public string PickupLocation { get; set; }
        public string Destination { get; set; }
        public decimal Weight { get; set; }
        public int Quantity { get; set; }
        public decimal? Total { get; set; } = 0;
        public string TransportMethod { get; set; }
        public int PricingId { get; set; }
        public string AdditionalServices { get; set; }
    }

    public class UpdateKoiOrderDto
    {
        public string PickupLocation { get; set; }
        public string Destination { get; set; }
        public decimal Weight { get; set; }
        public int Quantity { get; set; }
        public string TransportMethod { get; set; }
        public int PricingId { get; set; }
        public string AdditionalServices { get; set; }
    }
}
