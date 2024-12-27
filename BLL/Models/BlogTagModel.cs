using BLL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class BlogTagModel
    {
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public int[] SelectedTags { get; set; }
    }
}
