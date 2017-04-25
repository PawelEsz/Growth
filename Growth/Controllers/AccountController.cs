using Growth.DAL;
using Growth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Growth.Controllers
{
    public class AccountController : Controller
    {
        
        UserContext db = new UserContext();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            ViewBag.login = false;
            if (ModelState.IsValid)
            {
                if (isInDataBase(user.Email))
                {
                    var users = db.Users.Where(n => n.Email == user.Email).FirstOrDefault();
                    string password = users.Password;
                    if (password == user.Password)
                    {
                        ViewBag.login = true;
                        ViewBag.LoginInfo = "Udało się zalogować!";
                        return View();
                    }
                    else
                    {
                        ViewBag.LoginInfo = "Niepoprawny email lub hasło!";
                    }
                }
                else
                {
                    ViewBag.LoginInfo = "Konto o podanym emailu nie istnieje!";
                }
            }
            else ViewBag.LoginInfo = "Niepoprawnie wypełniony formularz!";
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Register(User user, string Conf)
        {
            if (ModelState.IsValid)
            {
                if (!isInDataBase(user.Email))
                {
                    string code = GenerateCode();

                    user.Code = code;
                    db.Users.Add(user);
                    db.SaveChanges();

                    SendConfirmedEmail(user.Name, user.Email, user.Password, code);

                    ViewBag.Registerinfo = "Udało się pomyślnie zarejestrować.\nSprawdź swoją skrzynkę pocztową!";
                    return View();
                }
                else
                {
                    ViewBag.Registerinfo = "Konto o podanym emailu juz istnieje!";
                    return View();
                }
            }
            else
            {
                ViewBag.Registerinfo = "Dane zostały niepoprawnie wprowadzone.";
                return View();
            }
        }

        bool isInDataBase(string email)
        {
            var emails = db.Users.Select(n => n.Email);
            foreach (var e in emails)
            {
                if (e.Equals(email)) return true;
            }
            return false;

        }

        public String GenerateCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for(int i =0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

        public static void SendConfirmedEmail(string name, string email, string password, string random)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("praczak535@gmail.com", ""); // :)
            smtp.EnableSsl = true;

            MailMessage msg = new MailMessage();
            msg.IsBodyHtml = true;
            msg.Subject = "Potwierdzenie rejestracji";
            msg.Body = "Cześć " + name + "!\nDziękuję Ci za rejestracje na mojej stronie. Póki co, nie zaimplementowałem jeszcze mechanizmu przywracania "
                + "hasła, więc gdyby jakimś cudem wyleciałoby Ci one z głowy, wróć tutaj w celu jego przypomnienia. (Lub napisz do mnie priv!) Twoje hasło to: " + password
                + "\nAby móc zalogować się do serwisu, potrzebujesz specjalnego kodu, który musisz podać przy pierwszym zalogowaniu. Skopiuj poniższy kod i "
                + "użyj go przy pierwszym logowaniu.\n KOD: " + random
                + "\nJeszcze raz dziekuję Ci za rejestracje. Mam nadzieję, że na mojej stronie znajdziesz coś co Cię zacikawi, zainspiruje i spędzisz tu dobrze czas!"
                + "\n\nPozdrawiam,\nPaweł!";
            string toAddress = email;
            msg.To.Add(email);

            string fromAddress = "\"Paweł Szopa :) \" <praczak.535@gmail.com>";
            msg.From = new MailAddress(fromAddress);

            try
            {
                smtp.Send(msg);
            }
            catch
            {
                throw;
            }
        }

        public ActionResult LogOut()
        {
            ViewBag.login = false;
            return RedirectToAction("Index", "Home");
        }
    }
}