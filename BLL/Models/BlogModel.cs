#nullable disable
using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class BlogModel
    {
        public Blog Record { get; set; }

        [DisplayName("Blog ID")]
        public int Id => Record.Id;

        [DisplayName("Blog Title")]
        public string Title => Record.Title;

        [DisplayName("Content")]
        public string Content => Record.Content;

        [DisplayName("Rating")]
        public decimal? Rating => Record.Rating;

        [DisplayName("Publish Date")]
        public DateTime PublishDate => Record.PublishDate;

        [DisplayName("User")]
        public string UserName => Record.User?.UserName;

        public UserModel User { get; set; }

        //Navigation 
        public ICollection<string> Tags
        {
            get
            {
                var tags = new List<string>();
                if (Record.BlogTags != null)
                {
                    foreach (var blogTag in Record.BlogTags)
                    {
                        tags.Add(blogTag.Tag?.Name);
                    }
                }
                return tags;
            }
        }
    }
}
