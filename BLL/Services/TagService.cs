using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;


namespace BLL.Services
{
    public interface ITagService
    {
        public IQueryable<TagModel> Query();
        public Service Create(Tag record);
        public Service Update(Tag record);
        public Service Delete(int id);

    }
    public class TagService : Service, ITagService
    {
        public TagService(Db db) : base(db)
        {
        }

        public Service Create(Tag record)
        {
            // Check if tag with the same name already exists
            var tags = _db.Tags.ToList();

            var existingTag = tags.FirstOrDefault(t => t.Name.Equals(record.Name, StringComparison.OrdinalIgnoreCase));


            if (existingTag != null)
            {
                // If the tag already exists, return a successful result with a message
                return Success("Tag already exists, skipping creation.");
            }

            // If the tag doesn't exist, create and save it
            _db.Tags.Add(record);
            _db.SaveChanges();

            return Success("Tag created successfully.");
        }

        public Service Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TagModel> Query()
        {
            return _db.Tags
                .Select(tag => new TagModel
                {
                    Record = tag
                });
        }

        public Service Update(Tag record)
        {
            // Find the existing tag by ID
            var existingTag = _db.Tags.FirstOrDefault(t => t.Id == record.Id);

            if (existingTag == null)
            {
                // If the tag doesn't exist, return an error
                return Error("Tag not found.");
            }

            // Update the tag properties
            existingTag.Name = record.Name;

            // Save changes to the database
            _db.SaveChanges();

            return Success("Tag updated successfully.");
        }
    }
}
