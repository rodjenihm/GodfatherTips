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

        public Role Role { get; set; }

        public bool IsVipMember { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime VipMembershipExpirationDate { get; set; }

        public IEnumerable<Post> Posts { get; set; }
    }
}
