using System.ComponentModel.DataAnnotations;

namespace PicWorldServer.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Bio { get; set; }
        public string ProfilePicture { get; set; }
        public List<Posts> Posts { get; set; }
    }
}
