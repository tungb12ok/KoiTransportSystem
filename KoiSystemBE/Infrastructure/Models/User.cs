using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public partial class User
    {
        public User()
        {
            CmsContents = new HashSet<CmsContent>();
            Feedbacks = new HashSet<Feedback>();
            KoiOrders = new HashSet<KoiOrder>();
        }

        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int? RoleId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<CmsContent> CmsContents { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<KoiOrder> KoiOrders { get; set; }
    }
}
