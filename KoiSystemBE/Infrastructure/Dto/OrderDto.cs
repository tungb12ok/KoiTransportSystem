using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dto
{
    public class OrderDto
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
        public List<OrderDocumentDto> OrderDocuments { get; set; }  // Chứng nhận liên quan đến đơn hàng
    }
}
