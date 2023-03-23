using Management_Debt.Context;
using Management_Debt.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Management_Debt.Controllers
{
    public class LoginController : Controller
    {
        ManagementDebtEntities db = new ManagementDebtEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Context.User user)
        {
            var hashFunction = new HashSaltWithRounds();
            byte[] salt = hashFunction.GenerateSalt();
            int numberOfIterations = 99;
            var hashPassword = hashFunction.HashDataWithRounds(Encoding.UTF8.GetBytes(user.password), salt, numberOfIterations);
			Context.User mUser = new Context.User();
            mUser.uid = Guid.NewGuid().ToString();
            mUser.username = user.username;
            mUser.email = user.email;
            mUser.confirm_email = "0";
            mUser.password = hashPassword;
            mUser.confirm_password = hashPassword;
            if(db.Users.Any(x => x.username == user.username))
            {

                ViewBag.Notification = "Tên đăng nhập đã được sử dụng";
                return View();
            }
            else
            {
                if(db.Users.Any(x => x.email == user.email))
                {
                    ViewBag.Notification = "Email đã được sử dụng";
                    return View();
                }
                else
                {
                    db.Users.Add(mUser);
                    db.SaveChanges();
                    ViewBag.Notification = "Đăng kí tài khoản thành công";
                    return View();
                }
            }
        }

        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Login(Context.User user)
        {
            
            var hashFunction = new HashSaltWithRounds();
            byte[] salt = hashFunction.GenerateSalt();
            int numberOfIterations = 99;
            var hashPassword = hashFunction.HashDataWithRounds(Encoding.UTF8.GetBytes(user.password), salt, numberOfIterations);
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = db.Users.Where(x => x.username.Equals(user.username) && x.password.Equals(hashPassword));
            if (result != null)
            {
                Session["UsernameCurrent"] = user.username.ToString();
                return RedirectToAction("Index", "Debtors");
            }
            else
            {
                ViewBag.Notification = "Tên đăng nhập hoặc mật khẩu không chính xác";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }










        public class HashSaltWithRounds
        {
            int saltLength = 32;
            public byte[] GenerateSalt()
            {
                using (var randomNumberGenerator = new RNGCryptoServiceProvider())
                {
                    var randomNumber = new byte[saltLength];
                    randomNumberGenerator.GetBytes(randomNumber);
                    return randomNumber;
                }
            }

            public string HashDataWithRounds(byte[] password, byte[] salt, int rounds)
            {
                using (var rfc2898 = new Rfc2898DeriveBytes(password, salt, rounds))
                {
                    return Convert.ToBase64String(rfc2898.GetBytes(32));
                }
            }
        }

    }
}