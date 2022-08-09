using Medical.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Medical.ViewModel
{
    public class CMemberViewModel
    {
        //=====================for PagedList使用
        public IPagedList<Member> mempage { set; get; }
        public IPagedList<RoleType> roleTypespage { set; get; }
        public IPagedList<Gender> MemGenderpage { set; get; }
        public IPagedList<City> MemCitypage { set; get; }
        //=================================
        public static string gmail { get; set; }
        public List<Member> mem { get; set; }
        public List<RoleType> roleTypes { get; set; }
        public List<Gender> MemGender { get; set; }
        public List<City> MemCity { get; set; }
        public CMemberViewModel()
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
        [Required(ErrorMessage = "身分證不可為空白")]
        public string IdentityId
        {
            get { return _member.IdentityId; }
            set { _member.IdentityId = value; }
        }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "密碼不可為空白")]
        public string Password
        {
            get { return _member.Password; }
            set { _member.Password = value; }
        }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "姓名不可為空白")]
        public string MemberName
        {
            get { return _member.MemberName; }
            set { _member.MemberName = value; }
        }

        [DisplayName("生日")]
        public DateTime? BirthDay
        {
            get { return _member.BirthDay; }
            set { _member.BirthDay = value; }
        }
        [DisplayName("性別")]
        public int? GenderId
        {
            get { return _member.GenderId; }
            set { _member.GenderId = value; }
        }

        [DisplayName("郵件信箱")]
        [EmailAddress(ErrorMessage = "不是正確的郵件格式")]
        [Required(ErrorMessage = "信箱不可為空白")]
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
        [DisplayName("權限")]
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
        [DisplayName("健保卡號")]
        public string IcCardNo
        {
            get { return _member.IcCardNo; }
            set { _member.IcCardNo = value; }
        }
        public int MemberId
        {
            get { return _member.MemberId; }
            set { _member.MemberId = value; }
        }
        //=================以下給Edit功能使用   (導覽屬性)
        //public virtual RoleType RoleT
        //{
        //    get { return _member.RoleNavigation; }
        //    set { _member.RoleNavigation = value; }
        //}
        //public virtual Gender GenD
        //{
        //    get { return _member.Gender; }
        //    set { _member.Gender = value; }
        //}

        //public virtual City City
        //{
        //    get { return _member.City; }
        //    set { _member.City = value; }
        //}
        //=====================
    }
}
