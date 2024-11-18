using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public partial class CmsContent
    {
        public int ContentId { get; set; }
        public int CreateBy { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? ContentType { get; set; }
        public string? Image { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual User CreateByNavigation { get; set; } = null!;
    }
}
