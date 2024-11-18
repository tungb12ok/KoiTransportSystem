using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dto
{
    public class UpdateOrderDocumentDto
    {
        public int DocumentId { get; set; }
        public string DocumentType { get; set; }
        public string DocumentPath { get; set; }
        public string Image { get; set; }
    }
}
