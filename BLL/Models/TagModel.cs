#nullable disable
using BLL.DAL;

namespace BLL.Models
{
    public class TagModel
    {
        public Tag Record { get; set; }

        public int Id => Record.Id;

        public string Name => Record.Name;

    }
}
