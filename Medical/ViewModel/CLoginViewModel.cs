using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class CLoginViewModel
    {
        [DisplayName("信箱")]
        [EmailAddress(ErrorMessage = "不是正確的郵件格式")]
        [Required(ErrorMessage = "請填寫電子郵件登入")]
        public string txtAccount { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "請填寫密碼")]
        public string txtPassword { get; set; }

        public string txtName { get; set; }
        public string reserve { get; set; }

    }
}
