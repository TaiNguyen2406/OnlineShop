using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using PagedList;
namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        [HttpGet]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
     
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            SetViewBag(user.GroupID);
            return View(user);
        }
        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var encryptedMD5Pas = Encrytor.MD5Hash(user.Password);
                user.Password = encryptedMD5Pas;
                long id = dao.Insert(user);
                if (id > 0)
                {
                    SetAlert("Thêm user thành công","success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Thêm user thất bại", "error");
                    ModelState.AddModelError("", "Thêm user thất bại");
                }
                SetViewBag();
                return View("Index");
            }
            else
            {
                SetAlert("Thêm user thất bại", "error");
                ModelState.AddModelError("", "Thêm user thất bại");
                return RedirectToAction("Index", "User");
            }
           
        }
        [HttpPost]
        //[ValidateInput(true)]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (!String.IsNullOrEmpty(user.Password))
                {
                    var encryptedMD5Pas = Encrytor.MD5Hash(user.Password);
                    user.Password = encryptedMD5Pas;

                }
                var result = dao.Update(user);
                if (result)
                {
                    SetAlert("Cập nhật  user thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Cập nhật user thất bại", "error");
                    ModelState.AddModelError("", "Cập nhật user thất bại");
                }
            }

            SetViewBag(user.GroupID);
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            SetViewBag();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus((id));
            return Json(new
            {
                status = result
            });
        }
        public void SetViewBag(string selectedId = null)
        {
            var dao = new UserGroupDao();
            ViewBag.GroupID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
        [HttpGet]
        public JsonResult IsEmailExist(string Email)
        {
            var users= new UserDao();
            bool isExist = false;
            if (users.ExitsMail(Email))
            {
                isExist = true;
            }
            return Json(!isExist, JsonRequestBehavior.AllowGet);
        }
    }
}