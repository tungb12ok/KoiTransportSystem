using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public partial class Pricing
    {
        public Pricing()
        {
            KoiOrders = new HashSet<KoiOrder>();
        }

        public int PriceId { get; set; }
        public string TransportMethod { get; set; } = null!;
        public string WeightRange { get; set; } = null!;
        public decimal PricePerKg { get; set; }
        public decimal? AdditionalServicePrice { get; set; }

        public virtual ICollection<KoiOrder> KoiOrders { get; set; }
    }
}
