using Google.Apis.Auth;
using Medical.Models;
using Medical.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.Json;
using System.Threading.Tasks;

namespace Medical.Controllers
{
    public class LoginController : Controller
    {
        private readonly MedicalContext _context;

        public LoginController(MedicalContext context)  //注入
        {
            _context = context;
        }
        //======================================================================


        public IActionResult LoginSuccess()
        {
            return View();
        }

        public IActionResult Login(string repath)
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USE))
            {
                return RedirectToAction("LoginSuccess");   //已登入的證明
            }
            else if (HttpContext.Session.Keys.Contains(CDictionary.SK_GOOGLELOGINED_USE))
                return RedirectToAction("LoginSuccess");
            if (repath != null)
            {
                ViewBag.reserve = "reserve";
            }

            //ViewData["ReUrl"] = reUrl;
            return View(new CLoginViewModel());

        }
        [HttpPost]
        public IActionResult Login(CLoginViewModel vModel)
        {
            string jasonUser = "";
            //先設定email登入
            //Member mb = (new MedicalContext()).Members.FirstOrDefault(n => n.Email == vModel.txtAccount);
            Member mb = (new MedicalContext()).Members.FirstOrDefault(n => n.Email.Equals(vModel.txtAccount));  //linQ不分大小寫

            //==================↓Google登入部分↓======================
            string googleUser = "";
            string? formCredential = Request.Form["credential"]; //回傳憑證
            string? formToken = Request.Form["g_csrf_token"]; //回傳令牌
            string? cookiesToken = Request.Cookies["g_csrf_token"]; //Cookie 令牌
                                                                    // 驗證 Google Token
            GoogleJsonWebSignature.Payload? payload = VerifyGoogleToken(formCredential, formToken, cookiesToken).Result;
            //==================↑Google登入部分↑======================

            if (mb != null)
            {

                if (mb.Email.Equals(vModel.txtAccount) && mb.Password.Equals(vModel.txtPassword) && vModel.reserve != null && mb.Role == 1)

                {
                    jasonUser = JsonSerializer.Serialize(mb);
                    HttpContext.Session.SetString(CDictionary.SK_LOGINED_USE, jasonUser);
                    //從預約頁面來的
                    return RedirectToAction("ReserveList", "Reserve");
                }

                if (mb.Email.Equals(vModel.txtAccount) && mb.Password.Equals(vModel.txtPassword) && mb.Role == 1)
                {
                    //LogbySession(mb);
                    jasonUser = JsonSerializer.Serialize(mb);
                    HttpContext.Session.SetString(CDictionary.SK_LOGINED_USE, jasonUser);
                    return RedirectToAction("Index", "Home");
                }
                else if (mb.Email.Equals(vModel.txtAccount) && mb.Password.Equals(vModel.txtPassword) && mb.Role == 2)
                {
                    jasonUser = JsonSerializer.Serialize(mb);
                    HttpContext.Session.SetString(CDictionary.SK_LOGINED_USE, jasonUser);
                    return RedirectToAction("List", "Consultation", new { area = "Doctors" });
                }
                else if (mb.Email.Equals(vModel.txtAccount) && mb.Password.Equals(vModel.txtPassword) && mb.Role == 3)
                {
                    jasonUser = JsonSerializer.Serialize(mb);
                    HttpContext.Session.SetString(CDictionary.SK_LOGINED_USE, jasonUser);
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
            }
            //==================↓Google登入部分↓======================
            else if (payload == null)
            {
                // 驗證失敗
                //ViewData["Msg"] = "授權失敗";
                googleUser = "授權失敗";
            }
            else if (payload != null)
            {
                CLoginViewModel cl = new CLoginViewModel();
                cl.txtName = payload.Name;
                //驗證成功，取使用者資訊內容
                //ViewData["Msg"] = "驗證 Google 授權成功" + "<br>";
                //ViewData["Msg"] += "Email:" + payload.Email + "<br>";
                //ViewData["Msg"] += "Name:" + payload.Name + "<br>";
                //ViewData["Msg"] += "Picture:" + payload.Picture;
                googleUser = JsonSerializer.Serialize(cl);
                HttpContext.Session.SetString(CDictionary.SK_GOOGLELOGINED_USE, googleUser);
                return RedirectToAction("Index", "Home");
            }
            //==================↑Google登入部分↑======================

            return View();
        }
        //=========================↓Google登入部分↓============================
        /// <summary>
        /// 驗證 Google Token
        /// </summary>
        /// <param name="formCredential"></param>
        /// <param name="formToken"></param>
        /// <param name="cookiesToken"></param>
        /// <returns></returns>
        //安裝套件Google.Apis.Auth
        public async Task<GoogleJsonWebSignature.Payload?> VerifyGoogleToken(string? formCredential, string? formToken, string? cookiesToken)
        {
            // 檢查空值
            if (formCredential == null || formToken == null && cookiesToken == null)
            {
                return null;
            }

            GoogleJsonWebSignature.Payload? payload;
            try
            {
                // 驗證 token
                if (formToken != cookiesToken)
                {
                    return null;
                }

                // 驗證憑證
                IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
                string GoogleApiClientId = Config.GetSection("GoogleApiClientId").Value;
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { GoogleApiClientId }
                };
                payload = await GoogleJsonWebSignature.ValidateAsync(formCredential, settings);
                if (!payload.Issuer.Equals("accounts.google.com") && !payload.Issuer.Equals("https://accounts.google.com"))
                {
                    return null;
                }
                if (payload.ExpirationTimeSeconds == null)
                {
                    return null;
                }
                else
                {
                    DateTime now = DateTime.Now.ToUniversalTime();
                    DateTime expiration = DateTimeOffset.FromUnixTimeSeconds((long)payload.ExpirationTimeSeconds).DateTime;
                    if (now > expiration)
                    {
                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
            return payload;
        }
        //=======================↑Google登入部分↑=============================
        public IActionResult Register()
        {
            CMemberViewModel memVModel = new CMemberViewModel()
            {
                mem = _context.Members.ToList(),
                roleTypes = _context.RoleTypes.ToList(),
                MemCity = _context.Cities.ToList(),
                MemGender = _context.Genders.ToList()
            };
            return View(memVModel);
        }

        [HttpPost]
        public IActionResult Register(CMemberViewModel vModel)
        {
            if (vModel.Email != null && vModel.Password != null)
            {

                sendMail();

                _context.Members.Add(vModel.member);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }


        private void sendMail()
        {
            //待寫入內容,註冊成功發送信件
        }


        public IActionResult Logout()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USE))
            {

                HttpContext.Session.Remove(CDictionary.SK_LOGINED_USE);
                return RedirectToAction("Index", "Home");

            }
            else if (HttpContext.Session.Keys.Contains(CDictionary.SK_GOOGLELOGINED_USE))
            {
                HttpContext.Session.Remove(CDictionary.SK_GOOGLELOGINED_USE);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        //================================================

        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgetPassword(CLoginViewModel vModel)  //參數=>收信者email
        {
            Member member = _context.Members.FirstOrDefault(q => q.Email == vModel.txtAccount);
            if (member != null)
            {
                CMemberViewModel.gmail = vModel.txtAccount;
                string account = "giraffegtest@gmail.com";
                string password = "kusbvagcbkfqcynb";
                SmtpClient client = new SmtpClient();
                //SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);  //這樣寫也可以，設定google server+port
                client.Host = "smtp.gmail.com"; //設定google server
                client.Port = 587;              //google port
                client.Credentials = new NetworkCredential(account, password);  //寄信人，內容寫在上方方便修改  //credential (n.)憑據；證書

                client.EnableSsl = true;           //是否啟用SSL驗證  =>SSL憑證是在網頁伺服器(主機)與網頁瀏覽器(客戶端)之間建立一個密碼連結的標準規範

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(account, "漢斯眼科");   //前面是發信email後面是顯示的名稱
                mail.To.Add(vModel.txtAccount);  //收信者email from 參數
                mail.Subject = "[漢斯眼科]一密碼重設通知信";  //標題
                mail.SubjectEncoding = System.Text.Encoding.UTF8;   //標題使用UTF8編碼
                mail.IsBodyHtml = true;   //內容使用html
                //mail.Body = $"<h1>漢斯眼科會員一{member.MemberName}，您好:</h1><br><h2>如欲重新設定密碼<a href='https://localhost:44302/Login/ResetPassword?email={vModel.txtAccount}'>請點我</a></h2>";
                mail.Body = $"<h1>漢斯眼科會員一{member.MemberName}，您好:</h1><br><h2>如欲重新設定密碼<a href='http://localhost:85/Login/ResetPassword?email={vModel.txtAccount}'>請點我</a></h2>";
                mail.BodyEncoding = System.Text.Encoding.UTF8;       //內文使用UTF8編碼
                try
                {
                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    mail.Dispose();
                    client.Dispose();//釋放資源
                }
                //return Content("<script>alert('信件已送出，請至信箱查看');window.location.href='https://localhost:44302/'</script>", "text/html", System.Text.Encoding.UTF8);    //localhost版本
                return Content("<script>alert('信件已送出，請至信箱查看');window.location.href='http://localhost:85/Home/index'</script>", "text/html", System.Text.Encoding.UTF8);  //IIS版本
                //window.location.href跳轉業面
            }
            else
                //return Content("<script>alert('未註冊的帳號，請確認輸入是否正確');window.location.href='https://localhost:44302/Login/ForgetPassword'</script>", "text/html", System.Text.Encoding.UTF8);
                return Content("<script>alert('未註冊的帳號，請確認輸入是否正確');window.location.href='http://localhost:85/Login/ForgetPassword'</script>", "text/html", System.Text.Encoding.UTF8);

        }

        public IActionResult ResetPassword(string email)
        {
            //把忘記密碼輸入的信箱傳入參數
            CLoginViewModel LogVM = new CLoginViewModel();
            Member mem = new Member();
                mem = _context.Members.Where(n => n.Email == email).FirstOrDefault();
        if (mem!=null)
            {
                LogVM.txtAccount = mem.Email;
                return View(LogVM);
            }
            else
                return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        public IActionResult ResetPassword(CLoginViewModel LogVM)
        {
            if (LogVM != null)
            {
                Member mem = _context.Members.Where(n => n.Email == LogVM.txtAccount).FirstOrDefault();
                mem.Password = LogVM.txtPassword;
                _context.SaveChanges();
                return Content("<script>alert('修改密碼成功');window.location.href='http://localhost:85/Home/index';</script>", "text/html", System.Text.Encoding.UTF8);
                //return Content("<script>alert('修改密碼成功');window.location.href='http://localhost/Home/index';</script>", "text/html", System.Text.Encoding.UTF8);
            }
            return RedirectToAction("Index", "Home");
        }


        public IActionResult LeadTobyRole()
        {
            string logJson = "";
            CMemberViewModel vm = null;
            int? roleID = -1;
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USE))
            {
                logJson = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USE);
                vm = JsonSerializer.Deserialize<CMemberViewModel>(logJson);
                roleID = vm.Role;
                if(roleID==1)   //導入修改頁面
                    return RedirectToAction("EditMember", "Login");
                else if(roleID==2)
                    return RedirectToAction("List", "Consultation", new { area = "Doctors" });
                else if(roleID==3)
                    return RedirectToAction("Index", "Home", new { area = "Admin" });

            }
            return RedirectToAction("Login", "Login");
        }
        public IActionResult EditMember()
        {
            string logJson = "";
            CMemberViewModel vm = null;
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USE))
            {
                logJson = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USE);
                vm = JsonSerializer.Deserialize<CMemberViewModel>(logJson);

                CMemberViewModel memVModel = new CMemberViewModel();
                memVModel.MemberId = _context.Members.FirstOrDefault(n => n.MemberId == vm.MemberId).MemberId;
                memVModel.MemberName = _context.Members.FirstOrDefault(n => n.MemberId == vm.MemberId).MemberName;
                memVModel.Email = _context.Members.FirstOrDefault(n => n.MemberId == vm.MemberId).Email;
                memVModel.BirthDay = _context.Members.FirstOrDefault(n => n.MemberId == vm.MemberId).BirthDay;
                memVModel.Address = _context.Members.FirstOrDefault(n => n.MemberId == vm.MemberId).Address;
                memVModel.Phone = _context.Members.FirstOrDefault(n => n.MemberId == vm.MemberId).Phone;
                //memVModel.Password = _context.Members.FirstOrDefault(n => n.MemberId == vm.MemberId).Password;
                memVModel.Password = "";    //要做修改，安全性考量給空字串
                memVModel.GenderId = _context.Members.FirstOrDefault(n => n.MemberId == vm.MemberId).GenderId;
                memVModel.CityId = _context.Members.FirstOrDefault(n => n.MemberId == vm.MemberId).CityId;
                memVModel.MemGender = _context.Genders.ToList();

                memVModel.MemCity = _context.Cities.ToList();


                return View(memVModel);
            }
        else
                return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult EditMember(CMemberViewModel vm)
        {
            //_context.Members.Add(vm.member);   //這樣寫會新增一筆
            Member mem = _context.Members.FirstOrDefault(c => c.MemberId == vm.MemberId);//這裡要等於vm.MemberId而不是@Html.ActionLink的id
            if (mem != null)
            {
                mem.MemberId = vm.MemberId;
                mem.MemberName = vm.MemberName;
                mem.Email = vm.Email;
                mem.Password = vm.Password;
                mem.BirthDay = vm.BirthDay;
                mem.GenderId = vm.GenderId;
                mem.IcCardNo = vm.IcCardNo;
                mem.Phone = vm.Phone;
                mem.CityId = vm.CityId;
                mem.Address = vm.Address;


                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
            //============================================AJAX API
            public IActionResult AccountCheck(string account)
        {
            if (!string.IsNullOrWhiteSpace(account))
            {
                Member mem = _context.Members.Where(n => n.Email == account).FirstOrDefault();
                if (mem != null)
                {
                    //return Content("此帳號已被使用", "text/html", System.Text.Encoding.UTF8);
                    return Content("used", "text/html", System.Text.Encoding.UTF8);
                }
                else
                {
                    //return Ok();
                    return Content("true", "text/html", System.Text.Encoding.UTF8);
                }
            }
            else
            {
                //return Content("請輸入帳號", "text/html", System.Text.Encoding.UTF8);
                return Content("false", "text/html", System.Text.Encoding.UTF8);
            }
        }

        public IActionResult LoginPwCheck(string pw,string mail)
        {
            if (!string.IsNullOrWhiteSpace(pw))
            {
                Member mem = _context.Members.Where(n => n.Password == pw&&n.Email==mail).FirstOrDefault();
                if (mem != null)
                {
                    return Ok();
                }
                else
                {    
                    return Content("密碼錯誤，請再試一次，或按 [忘記密碼] 以重設密碼。", "text/html", System.Text.Encoding.UTF8);
                }
            }
            else
            {
                return Content("請輸入密碼", "text/html", System.Text.Encoding.UTF8);
            }
        }

        public IActionResult EditPwCheck(string pw, string mail)
        {
            if (!string.IsNullOrWhiteSpace(pw))
            {
                Member mem = _context.Members.Where(n => n.Password == pw && n.Email == mail).FirstOrDefault();
                if (mem != null)
                {
                    return Content("true", "text/html", System.Text.Encoding.UTF8);
                }
                else
                {
                    return Content("false", "text/html", System.Text.Encoding.UTF8);
                }
            }
            else
            {
                return Content("false", "text/html", System.Text.Encoding.UTF8);
            }
        }
    }
}
