using Medical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class CMemberAdminViewModel
    {
        private Member _Member;

        public CMemberAdminViewModel()
        {
            _Member = new Member();
        }
        public List<Member> mem { get; set; }
        public Member Member
        {
            get { return _Member; }
            set { _Member = value; }
        }
        public int MemberId
        {
            get { return _Member.MemberId; }
            set { _Member.MemberId = value; }
        }

        public string IdentityId
        {
            get { return _Member.IdentityId; }
            set { _Member.IdentityId = value; }
        }
        public string Password
        {
            get { return _Member.Password; }
            set { _Member.Password = value; }
        }
        public string MemberName
        {
            get { return _Member.MemberName; }
            set { _Member.MemberName = value; }
        }
        public DateTime? BirthDay
        {
            get { return _Member.BirthDay; }
            set { _Member.BirthDay =value; }
        }
        public int? GenderId
        {
            get { return _Member.GenderId; }
            set { _Member.GenderId = value; }
        }
        public string BloodType
        {
            get { return _Member.BloodType; }
            set { _Member.BloodType = value; }
        }
        public int? Weight
        {
            get { return _Member.Weight; }
            set { _Member.Weight = value; }
        }
        public string IcCardNo
        {
            get { return _Member.IcCardNo; }
            set { _Member.IcCardNo = value; }
        }
        public string Email
        {
            get { return _Member.Email; }
            set { _Member.Email = value; }
        }
        public string Phone
        {
            get { return _Member.Phone; }
            set { _Member.Phone = value; }
        }

        public int? Role
        {
            get { return _Member.Role; }
            set { _Member.Role = value; }
        }
        public int? CityId
        {
            get { return _Member.CityId; }
            set { _Member.CityId = value; }
        }
        public string Address
        {
            get { return _Member.Address; }
            set { _Member.Address = value; }
        }
    }
}