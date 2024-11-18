using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dto
{
    public class CMSContentDto
    {
        public int ContentId { get; set; }
        public int CreatedBy { get; set; }
        public  string CreateByName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
