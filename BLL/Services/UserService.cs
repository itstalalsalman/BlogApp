using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace BLL.Services
{
     
    public interface IUserService
    {
        public IQueryable<UserModel> Query();
        public Service Create(User user);
        public Service Update(User user);
        public Service Delete(int id);
    }

    public class UserService : Service, IUserService
    {
        public UserService(Db db) : base(db)
        {
        }

        public Service Create(User user)
        {
            if (_db.Users.Any(p => p.UserName == null))
                return Error("Username cannot be empty!");
            if (_db.Users.Any(b => b.UserName.ToUpper() == user.UserName.ToUpper().Trim()))
                return Error("Username with the same name already exists!");
            var role = _db.Roles.FirstOrDefault(r => r.Id == user.RoleId);
            if (role == null)
                return Error("Invalid Role selected!");

            user.Role = role;
            _db.Users.Add(user);
            _db.SaveChanges();
            return Success("User created successfully.");
        }

        public Service Delete(int id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return Error("User not found.");
            
            _db.Users.Remove(user);
            _db.SaveChanges();
            return Success("User deleted successfully.");
        }

        public IQueryable<UserModel> Query()
        {
            return _db.Users
                .Include(u => u.Role)
                .Select(u => new UserModel { Record = u, RoleName = u.Role.Name });
        }

        public Service Update(User user)
        {
            var existingUser = _db.Users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser == null)
                return Error("User not found.");

            existingUser.UserName = user.UserName;
            existingUser.Password = user.Password;
            existingUser.IsActive = user.IsActive;
            existingUser.RoleId = user.RoleId;

            _db.SaveChanges();
            return Success("User updated successfully.");
        }
    }
}