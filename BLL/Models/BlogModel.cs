#nullable disable
using BLL.DAL;
using System.Collections.Generic;
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

        public int UserId => Record.UserId;

        public UserModel User { get; set; }

        //Navigation 
        [DisplayName("Tags")]
        public ICollection<string> Tags { get; set; } = new List<String>();

        [DisplayName("Tags (Comma Separated)")]
		public string TagNames { get; set; }

		[DisplayName("Tag IDs")]
		public List<int> TagIds { get; set; }

        public virtual ICollection<BlogTag> BlogTags { get; set; }
    }
}


