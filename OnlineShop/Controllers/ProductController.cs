using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }

        public ActionResult Category(long cateID)
        {
            var category = new CategoryDao().ViewDetail(cateID);
            return View(category);
        }
        public ActionResult Detail(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            return View(product);
        }
    }
}