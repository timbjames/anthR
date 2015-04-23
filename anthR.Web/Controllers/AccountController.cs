using anthR.Web.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace anthR.Web.Controllers
{
        
    public class AccountController : Controller
    {
        
        private anthRContext _db = new anthRContext();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult LogOn()
        {            
            return View();
        }
                
        [AllowAnonymous]
        [HttpPost]
        public ActionResult LogOn(ViewModels.LoginViewModel model)
        {
            
            if (ModelState.IsValid)
            {                
                // need to validate username and pword in db
                var staffMember = _db.Staff.Where(s => s.Username.Equals(model.Username, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (string.IsNullOrEmpty(staffMember.Password))
                {
                    // just set the password and login user.
                    staffMember.Password = EncryptPassword(model.Password);
                    _db.SaveChanges();

                    FormsAuthentication.SetAuthCookie(staffMember.Username, true);

                    return RedirectToAction("Index", new { controller = "Projects" });
                }
                else
                {
                    if (IsValid(model.Password, staffMember.Password))
                    {
                        FormsAuthentication.SetAuthCookie(staffMember.Username, true);
                        return RedirectToAction("Index", new { controller = "Projects" });
                    }
                }
            }

            return View(model);

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogOn");
        }

        private bool IsValid(string password, string encryptedPassword)
        {
            return encryptedPassword.Equals(EncryptPassword(password));
        }

        private string EncryptPassword(string password)
        {
            // encrtype the password
            return anthR.Utils.Crypto.MD5.Encrypt(password, "Kfisher1", true);            
        }

        private List<Claim> GetClaims(Staff staff)
        {
            
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, staff.Email));           
            claims.Add(new Claim(ClaimTypes.Name, staff.Name));
             
            var roles = new[] { "Admin", "Citizin", "Worker" };
            var groups = new[] { "Admin", "Citizin", "Worker" };
             
            return claims;

        }

    }
}