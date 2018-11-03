using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Model.Dao;
using Model.EF;
using OnlineShop.Models;
namespace OnlineShop.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            var model = new ContactDao().GetActiveContact();
            return View(model);
        }
        [HttpPost]
        public JsonResult Send(string name, string mobile, string address, string email, string content)
        {
            var feedback = new Feedback();
            feedback.Name = name;
            feedback.Email = email;
            feedback.CreatedDate = DateTime.Now;
            feedback.Phone = mobile;
            feedback.Content = content;
            feedback.Address = address;

            var id = new ContactDao().InsertFeedBack(feedback);
            if (id > 0)
            {
                string mail = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/newfeedback.html"));

                mail = mail.Replace("{{CustomerName}}", name);
                mail = mail.Replace("{{Phone}}", mobile);
                mail = mail.Replace("{{Email}}", email);
                mail = mail.Replace("{{Address}}", address);
                mail = mail.Replace("{{Content}}", content);
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(toEmail, "Phản hồi mới từ OnlineShop", mail);
                return Json(new
                {
                    status = true
                });
             
            }

            else
                return Json(new
                {
                    status = false
                });

        }
    }
}