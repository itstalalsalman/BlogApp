using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace BLL.Services
{
    
    public class UserService : Service, IService<User, UserModel>
	{
        public UserService(Db db) : base(db)
        {
        }

        public Service Create(User user)
        {
			throw new NotImplementedException();
		}

        public Service Delete(int id)
        {
			throw new NotImplementedException();
		}

        public IQueryable<UserModel> Query()
        {
			return _db.Users.Include(u => u.Role).OrderByDescending(u => u.IsActive).ThenBy(u => u.UserName).Select(u => new UserModel()
			{
				Record = u
			});
		}

        public Service Update(User user)
        {
			throw new NotImplementedException();
		}
	}
}