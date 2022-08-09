using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class RoleType
    {
        public RoleType()
        {
            Members = new HashSet<Member>();
        }

        public int Role { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}
