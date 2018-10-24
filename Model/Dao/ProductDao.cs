using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Model.EF;
using Model.ViewModel;
namespace Model.Dao
{
    public class ProductDao
    {
         OnlineShopDbContext db = null;

        public ProductDao()
        {
            db= new OnlineShopDbContext();
        }
        /// <summary>
        /// lấy list product by category
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        ///
        public List<ProductViewModel> ListByCategoryId(long categoryID, ref int totalRecord, int pageIndex=1,int pageSize=1)
        {
            totalRecord = db.Products.Where(x => x.CategoryID == categoryID).Count();
           // var model2 = db.Products.Where(x => x.CategoryID == categoryID).OrderBy(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            var model = from a in db.Products
                join b in db.ProductCategories on a.CategoryID equals b.ID
                where a.CategoryID == categoryID
                select new ProductViewModel()
                {
                    CateMetaTitle = b.MetaTitle,
                    CateName = b.Name,
                    CreateDate = a.CreatedDate,
                    ID = a.ID,
                    Name = a.Name,
                    Image = a.Image,
                    MetaTitle = a.MetaTitle,
                    Price = a.Price
                };
            model = model.OrderBy(x => x.CreateDate).Skip((pageIndex - 1) * pageSize).Take((pageSize));
            return model.ToList();
        }
        public List<Product> ListNewProduct(int n)
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).Take(n).ToList();
        }
            
        public List<Product> ListFeatureProduct(int n)
        {
            return db.Products.Where(x=>x.TopHot!=null && x.TopHot>DateTime.Now).OrderByDescending(x => x.CreatedDate).Take(n).ToList();
        }

        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }
      
    }
}
