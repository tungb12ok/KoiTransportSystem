using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dto
{
    public class UpdatePriceDto
    {
        public string TransportMethod { get; set; }
        public string WeightRange { get; set; }
        public decimal PricePerKg { get; set; }
        public decimal? AdditionalServicePrice { get; set; }
    }

}
