using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dto
{
    public class HealthCheckOrderDto
    {
        public int OrderId { get; set; }
        public string PickupLocation { get; set; }
        public string Destination { get; set; }
        public decimal Weight { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }
}
