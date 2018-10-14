using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Model.EF;
using PagedList;

namespace Model.Dao
{
    public class ProductCategoryDao
    {
        OnlineShopDbContext db = null;
        public ProductCategoryDao()
        {
            db = new OnlineShopDbContext();
        }
        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public long Insert(ProductCategory entity)
        {
            if (entity.CreatedDate == null)
                entity.CreatedDate = DateTime.Now;
            entity.MetaTitle = convertToUnSign3(entity.Name);
            db.ProductCategories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(ProductCategory entity)
        {
            try
            {
                var ProductCategory = db.ProductCategories.Find(entity.ID);
                ProductCategory.Name = entity.Name;
                ProductCategory.Status = entity.Status;
                ProductCategory.ModifiedBy = entity.ModifiedBy;
                ProductCategory.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool ChangeStatus(long id)
        {
            var ProductCategory = db.ProductCategories.Find(id);
            ProductCategory.Status = !ProductCategory.Status;
            db.SaveChanges();
            return ProductCategory.Status;
        }
        public bool Delete(long id)
        {
            try
            {
                var ProductCategory = db.ProductCategories.Find(id);
                db.ProductCategories.Remove(ProductCategory);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public IEnumerable<ProductCategory> ListAllPaging(string searchStrimg, int page, int pageSize)
        {
            IQueryable<ProductCategory> model = db.ProductCategories;
            if (!string.IsNullOrEmpty(searchStrimg))
            {
                model = model.Where(x => x.Name.Contains(searchStrimg));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
        public ProductCategory GetbyId(int id)
        {
            return db.ProductCategories.SingleOrDefault(x => x.ID == id);
        }
        public ProductCategory ViewDetail(string id)
        {
            return db.ProductCategories.Find(id);
        }
        public List<ProductCategory> ListAll()
        {
            return db.ProductCategories.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
         
    }
}
