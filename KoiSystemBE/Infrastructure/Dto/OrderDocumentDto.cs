using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dto
{
    public class OrderDocumentDto
    {
        public int DocumentId { get; set; }
        public string DocumentType { get; set; }  // Ví dụ: "Sức khỏe", "Nguồn gốc"
        public string DocumentPath { get; set; }  // Đường dẫn file chứng nhận
        public DateTime CreatedAt { get; set; }
    }

}
