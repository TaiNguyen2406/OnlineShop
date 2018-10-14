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
    public class ProductCategoryController : BaseController
    {
        [HttpGet]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {

            var dao = new ProductCategoryDao();
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
            var ProductCategory = new ProductCategoryDao().ViewDetail(id);
            return View(ProductCategory);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductCategoryDao();
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.CreatedBy = session.UserName;
                long id = dao.Insert(model);
                SetAlert("Thêm category product thành công", "success");
                return RedirectToAction("Index", "ProductCategory");
            }
            return View("Index");

        }
        [HttpPost]
        public ActionResult Edit(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductCategoryDao();
                var result = dao.Update(model);
                if (result)
                {
                    SetAlert("Cập nhật category product thành công", "success");
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    SetAlert("Cập nhật category product thất bại", "error");
                    ModelState.AddModelError("", "Cập nhật user group thất bại");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new ProductCategoryDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}