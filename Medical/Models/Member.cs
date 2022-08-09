using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class Member
    {
        public Member()
        {
            ArticleComments = new HashSet<ArticleComment>();
            CaseRecords = new HashSet<CaseRecord>();
            Doctors = new HashSet<Doctor>();
            Orders = new HashSet<Order>();
            Reserves = new HashSet<Reserve>();
            Reviews = new HashSet<Review>();
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public int MemberId { get; set; }
        public string IdentityId { get; set; }
        public string Password { get; set; }
        public string MemberName { get; set; }
        public DateTime? BirthDay { get; set; }
        public int? GenderId { get; set; }
        public string BloodType { get; set; }
        public int? Weight { get; set; }
        public string IcCardNo { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? Role { get; set; }
        public int? CityId { get; set; }
        public string Address { get; set; }

        public virtual RoleType RoleNavigation { get; set; }
        public virtual ICollection<ArticleComment> ArticleComments { get; set; }
        public virtual ICollection<CaseRecord> CaseRecords { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Reserve> Reserves { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
