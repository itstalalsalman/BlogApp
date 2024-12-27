using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{

    public class BlogService : Service, IService<Blog, BlogModel>
    {
        public BlogService(Db db) : base(db)
        {

        }

        public IQueryable<BlogModel> Query()
        {
            return _db.Blogs
                .Include(b => b.User)
                .Select(b => new BlogModel { Record = b });
        }

        public Service Create(Blog blog)
        {
            if (_db.Blogs.Any(p => p.Title == null))
                return Error("Blog title cannot be empty!");
            if (_db.Blogs.ToList().Any(b => b.Title.Equals(blog.Title, StringComparison.OrdinalIgnoreCase))) // String comparison was causing issues so used the autosuggestion by vs studio
                return Error("Blog with the same title already exists!");
            if (blog.Rating < 0 || blog.Rating > 10)
                return Error("Rating should be between 0 and 10");

            _db.Blogs.Add(blog);
            _db.SaveChanges();
            return Success("Blog created successfully.");
        }

        public Service Update(Blog blog)
        {
            if (_db.Blogs.ToList().Any(b => b.Id != blog.Id && b.Title.Equals(blog.Title, StringComparison.OrdinalIgnoreCase)))
                return Error("Blog with the same title already exists!");
            var entity = _db.Blogs.FirstOrDefault(b => b.Id == blog.Id);
            if (entity is null)
                return Error("Blog not found!");

            entity.Title = blog.Title?.Trim();
            entity.Content = blog.Content;
            entity.Rating = blog.Rating;
            entity.PublishDate = blog.PublishDate;
            entity.UserId = blog.UserId;
            _db.Blogs.Update(entity);
            _db.SaveChanges();
            return Success("Blog updated successfully.");
        }

        public Service Delete(int id)
        {
            var entity = _db.Blogs.FirstOrDefault(b => b.Id == id);
            if (entity is null)
                return Error("Blog not found!");

            _db.Blogs.Remove(entity);
            _db.SaveChanges();
            return Success("Blog deleted successfully.");
        }
    }
}
