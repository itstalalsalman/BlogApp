using System.ComponentModel;

namespace BLL.Models
{
    public class FavoritesModel
    {
        public int BlogId { get; set; }
        public int UserId { get; set; }

        [DisplayName("Blog Name")]
        public string BlogName { get; set; }
    }
}