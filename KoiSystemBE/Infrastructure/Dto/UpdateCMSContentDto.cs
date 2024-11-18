using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dto
{
    public class UpdateCMSContentDto
    {
        public string Title { get; set; }           // Tiêu đề của nội dung
        public string Content { get; set; }         // Nội dung chính (chi tiết)
        public string ContentType { get; set; }     // Loại nội dung: dịch vụ, tin tức, FAQ, etc.
        public string Image { get; set; }           // Đường dẫn hình ảnh (nếu có)
        public DateTime UpdatedAt { get; set; }     // Ngày cập nhật nội dung
    }
}
