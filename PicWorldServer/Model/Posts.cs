using System.ComponentModel.DataAnnotations;

namespace PicWorldServer.Model
{
    public class Posts
    {
        [Key]
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int Liked { get; set; }
        public string ImageLink { get; set; }
        public string ImageDescription { get; set; }

    }
}
