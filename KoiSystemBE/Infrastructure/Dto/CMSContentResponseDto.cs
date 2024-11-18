using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dto
{
    public class CMSContentResponseDto
    {
        public int ContentId { get; set; }        // ID của nội dung
        public string Title { get; set; }         // Tiêu đề
        public string Content { get; set; }       // Nội dung chi tiết
        public string ContentType { get; set; }   // Loại nội dung: Dịch vụ, FAQ, Tin tức
        public string Image { get; set; }         // Hình ảnh liên quan (nếu có)
        public DateTime CreatedAt { get; set; }   // Ngày tạo
        public DateTime? UpdatedAt { get; set; }  // Ngày cập nhật (nếu có)
    }

}
