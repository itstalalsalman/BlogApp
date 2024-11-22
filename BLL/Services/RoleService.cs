using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using System;
using System.Linq;
using System.Reflection.Metadata;

namespace BLL.Services
{
    public interface IRoleService
    {
        Service Create(Role role);
        Service Update(Role role);
        Service Delete(int id);
        IQueryable<RoleModels> Query();
    }

    public class RoleService : Service, IRoleService
    {
        public RoleService(Db db) : base(db)
        {
        }

        // Create a new role
        public Service Create(Role role)
        {
            if (_db.Roles.Any(p => p.Name == null))
                return Error("Role Name cannot be empty!");
            
            _db.Roles.Add(role);
            _db.SaveChanges();
            return Success("Blog created successfully.");
        }

        // Update an existing role
        public Service Update(Role role)
        {
            var entity = _db.Roles.FirstOrDefault(r => r.Id == role.Id);
            if (entity == null)
            {
                return Error("Role not found.");
            }

            entity.Name = role.Name;
            _db.SaveChanges();
            return Success("Role updated successfully.");
        }

        // Delete a role by ID
        public Service Delete(int id)
        {
            var role = _db.Roles.FirstOrDefault(r => r.Id == id);
            if (role == null)
            {
                return Error("Role Id not found.");
            }

            
            
             _db.Roles.Remove(role);
             _db.SaveChanges();
             return Success("Role deleted successfully.");
            
        }

        // Query to get all roles or filter as needed
        public IQueryable<RoleModels> Query()
        {
            return _db.Roles.Select(r => new RoleModels { Record = r });
        }
    }
}
