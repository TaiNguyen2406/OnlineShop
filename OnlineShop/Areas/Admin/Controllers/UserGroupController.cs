using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EF;
using OnlineShop.Common;
namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserGroupController : BaseController
    {
        // GET: Admin/UserGroup
        [HttpGet]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new UserGroupDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(string id)
        {
            var userGroup = new UserGroupDao().ViewDetail(id);
            return View(userGroup);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(UserGroup model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserGroupDao();
                string id = dao.Insert(model);
                if (id != null)
                {
                    SetAlert("Thêm user group thành công", "success");
                    return RedirectToAction("Index", "UserGroup");
                }
                else
                {
                    SetAlert("Thêm user group thất bại", "error");
                    ModelState.AddModelError("", "Thêm user thất bại");
                }
            }
            return View("Index");

        }
        [HttpPost]
        public ActionResult Edit(UserGroup model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserGroupDao();
                var result = dao.Update(model);
                if (result)
                {
                    SetAlert("Cập nhật  user group thành công", "success");
                    return RedirectToAction("Index", "UserGroup");
                }
                else
                {
                    SetAlert("Cập nhật user group thất bại", "error");
                    ModelState.AddModelError("", "Cập nhật user group thất bại");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(string id)
        {
            new UserGroupDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}