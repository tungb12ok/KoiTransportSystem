using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dto
{
    public class TransportProcessDto
    {
        public int ProcessId { get; set; }
        public string Step { get; set; }
        public string Status { get; set; }
        public DateTime ProcessDate { get; set; }
    }

    public class KoiOrderDetailDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string PickupLocation { get; set; }
        public string Destination { get; set; }
        public decimal Weight { get; set; }
        public int Quantity { get; set; }
        public string TransportMethod { get; set; }
        public string AdditionalServices { get; set; }
        public string Status { get; set; }
        public DateTime PlacedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public List<TransportProcessDto> TransportProcesses { get; set; }
    }
}
