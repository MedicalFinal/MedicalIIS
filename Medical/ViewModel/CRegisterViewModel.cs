using Medical.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class CRegisterViewModel
    {
        public static string gmail { get; set; }
        public List<Member> mem { get; set; }
        public List<RoleType> roleTypes { get; set; }
        public List<Gender> MemGender { get; set; }
        public List<City> MemCity { get; set; }
        public CRegisterViewModel()
        {
            _member = new Member();
            _gender = new Gender();
        }
        private Member _member;
        private Gender _gender;
        public Member member
        {
            get { return _member; }
            set { _member = value; }
        }
        public Gender gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        [DisplayName("身分證字號")]
        [Required(ErrorMessage = "必填")]
        public string IdentityId
        {
            get { return _member.IdentityId; }
            set { _member.IdentityId = value; }
        }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "必填")]
        public string Password
        {
            get { return _member.Password; }
            set { _member.Password = value; }
        }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "必填")]
        public string MemberName
        {
            get { return _member.MemberName; }
            set { _member.MemberName = value; }
        }

        [DisplayName("生日")]
        public DateTime? BirthDay
        {
            get { return _member.BirthDay; }
            set { _member.BirthDay =value; }
        }
        [DisplayName("性別")]
        public int? GenderId
        {
            get { return _member.GenderId; }
            set { _member.GenderId = value; }
        }

        [DisplayName("郵件信箱")]
        public string Email
        {
            get { return _member.Email; }
            set { _member.Email = value; }
        }
        [DisplayName("手機號碼")]
        public string Phone
        {
            get { return _member.Phone; }
            set { _member.Phone = value; }
        }
        public int? Role

        {
            get { return _member.Role; }
            set { _member.Role = value; }
        }
        [DisplayName("縣市")]
        public int? CityId
        {
            get { return _member.CityId; }
            set { _member.CityId = value; }
        }
        [DisplayName("地址")]
        public string Address
        {
            get { return _member.Address; }
            set { _member.Address = value; }
        }
    }
}