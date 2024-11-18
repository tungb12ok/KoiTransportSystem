using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dto
{
    public class CreateCMSContentDto
    {
        [Required]
        public int CreateBy { get; set; } // ID của người dùng phải tồn tại trong bảng Users
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string ContentType { get; set; }
        public string Image { get; set; }
    }

}
