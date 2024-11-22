#nullable disable
using BLL.DAL;


namespace BLL.Models
{
    public class BlogTagModel
    {
        public BlogTag Record { get; set; }

        public int Id => Record.Id;

        public int BlogId => Record.BlogId;

        public int TagId => Record.TagId;

        // Navigation Properties
        public BlogModel Blog { get; set; }
        public TagModel Tag { get; set; }

        // Constructor for initialization
        public BlogTagModel()
        {
            Blog = new BlogModel();
            Tag = new TagModel();
        }

    }
}
