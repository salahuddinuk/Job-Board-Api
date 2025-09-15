using System.ComponentModel.DataAnnotations;

namespace JobBoardAPI.Models
{
    public sealed class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }

        public User(int id, string email, string password, bool isActive) {
            Id = id;
            Email = email;
            Password = password;
            CreatedOn = DateTime.Now;
            IsActive = isActive;            
        }
    }
}
