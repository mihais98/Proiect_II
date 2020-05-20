using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using RegistrationAndLogin.Models;
using System.Web.Security;

using RegistrationAndLogin.Service;
using RegistrationAndLogin.Repository;

namespace RegistrationAndLogin.Controllers
{

    public class UserController : Controller
    {
        IUserService userService;
        

      

        public UserController()
        {
            userService = new UserService(new UserRepository());
        }


        //Registration Action

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }


        //Registration POST Action

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode")] User user)
        {
            bool Status = false;
            string message = "";
            //
            
            //Model Validation
            if(ModelState.IsValid)
            {
                //Email is already exist

                var isExist = userService.IsEmailExist(user.EnailID);
                if(isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);

                }
                #region Generate Activation Code
                user.ActivationCode = Guid.NewGuid();

                #endregion

                #region Password Hashing
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword); ;
                #endregion
                user.IsEmailVerified = false;

                #region Save to Datebase
                DatabaseCon con = new DatabaseCon();
                var dc = con.Access();
                
                    dc.Users.Add(user);
                    dc.SaveChanges();

                    //send email to user
                    SendVerificationLink(user.EnailID, user.ActivationCode.ToString());
                    message = "Registration successfully done. Account activation link has been sent to your email id :" + user.EnailID;
                    Status = true; 
                
                #endregion

            }
            else
            {
                message = "invalid request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;

            //send email to user

            return View(user);
        }

        

        //Verify Account
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            DatabaseCon con = new DatabaseCon();
            var dc = con.Access();
            
            
                dc.Configuration.ValidateOnSaveEnabled = false;

                var v = dc.Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();

                if (v!=null)
                {
                    v.IsEmailVerified = true;
                    dc.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }

            
            ViewBag.Status = Status;

            return View();

        }



        //Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl = "")
        {
            string message = "";

            DatabaseCon con = new DatabaseCon();
            var dc = con.Access();

           
                var v = userService.FindByEmail(login.EnailID);
                if (v != null)
                {
                    if (!v.IsEmailVerified)
                    {
                        ViewBag.Message = "Please verify your email first";
                        return View();
                    }

                    if (string.Compare(Crypto.Hash(login.Password), v.Password) == 0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20; // 525600 min = 1 year
                        var ticket = new FormsAuthenticationTicket(login.EnailID, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);


                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        message = "Invalid credential provided";
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
            
            ViewBag.Message = message;
            return View();
        }

        //Logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            DatabaseCon con = new DatabaseCon();
            var dc = con.Access();
           
            
                var v = dc.Users.Where(a => a.EnailID == emailID).FirstOrDefault();
                return v != null;
            
        }

        [NonAction]
        public void SendVerificationLink(string emailID,string activationCode)
        {
            var verifyUrl = "/User/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("mihais.photography@gmail.com", "MihaiS Photography");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "liceminescu2"; // Replace with actual password
            string subject = "Your account is successfully created!";

            string body = "<br/><br/>We are excited to tell you that your MihaiS Photography account is" +
                " successfully created. Please click on the below link to verify your account" +
                " <br/><br/><a href='" + link + "'>" + link + "</a> ";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

    }
   
}