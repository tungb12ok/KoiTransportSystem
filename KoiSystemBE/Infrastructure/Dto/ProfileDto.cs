using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dto
{
    public class ProfileDto
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
