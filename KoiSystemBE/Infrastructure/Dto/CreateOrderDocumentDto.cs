using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dto
{
    public class CreateOrderDocumentDto
    {
        public int OrderId { get; set; }
        public string DocumentType { get; set; }
        public string DocumentPath { get; set; }
        public string Image { get; set; }
    }
}
