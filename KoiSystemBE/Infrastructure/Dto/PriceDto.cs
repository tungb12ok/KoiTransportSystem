using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dto
{
    public class PriceDto
    {
        public int PriceId { get; set; }
        public string TransportMethod { get; set; }
        public string WeightRange { get; set; }
        public decimal PricePerKg { get; set; }
        public decimal? AdditionalServicePrice { get; set; }
    }
}
