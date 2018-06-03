using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Model.EF;

namespace Model.Dao
{
    public class UserDao
    {
        private OnlineShopDbContext db = null;

        public UserDao()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public IEnumerable<User> ListAllPaging(int page, int pageSize)
        {
            return db.Users.OrderByDescending(x=>x.CreatedDate).ToPagedList(page, pageSize);
        }
        public User GetbyId(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }
        public int Login(string userName, string passWord)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);//&& x.Password == passWord);
            if (result == null)
            {
                return 0;//"Tài khoản không tồn tại";
            }
            else
            {
                if (result.Status == false)
                {
                    return -1;//"Tài khoản bị khóa";
                }
                else
                {
                    if (result.Password == passWord)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }

        }
    }
}
