using GodfatherTips.Utilities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GodfatherTips.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nickname { get; set; }

        public Role Role { get; set; } = Role.Member;

        public bool IsVipMember { get; set; } = false;

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        public DateTime VipMembershipExpirationDate { get; set; }

        public IEnumerable<Post> Posts { get; set; }
    }
}
