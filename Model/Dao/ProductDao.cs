using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
namespace Model.Dao
{
    public class ProductDao
    {
         OnlineShopDbContext db = null;

        public ProductDao()
        {
            db= new OnlineShopDbContext();
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
