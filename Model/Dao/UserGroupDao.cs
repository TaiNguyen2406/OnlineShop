using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Model.EF;
namespace Model.Dao
{
    public class UserGroupDao
    {
        private OnlineShopDbContext db = null;
        public UserGroupDao()
        {
            db = new OnlineShopDbContext();
        }
        public string Insert(UserGroup entity)
        {
            if (entity.ID == null)
                return null;
            db.UserGroups.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(UserGroup entity)
        {
            try
            {
                var userGroup = db.UserGroups.Find(entity.ID);
                userGroup.Name = entity.Name;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool Delete(string id)
        {
            try
            {
                var userGroup = db.UserGroups.Find(id);
                db.UserGroups.Remove(userGroup);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public IEnumerable<UserGroup> ListAllPaging(string searchStrimg, int page, int pageSize)
        {
            IQueryable<UserGroup> model = db.UserGroups;
            if (!string.IsNullOrEmpty(searchStrimg))
            {
                model = model.Where(x => x.ID.Contains(searchStrimg) || x.Name.Contains(searchStrimg));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
        public UserGroup GetbyId(string id)
        {
            return db.UserGroups.SingleOrDefault(x => x.ID == id);
        }
        public UserGroup ViewDetail(string id)
        {
            return db.UserGroups.Find(id);
        }
        public List<UserGroup> ListAll()
        {
            return db.UserGroups.ToList();
        }
    }
}
