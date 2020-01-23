using System.ComponentModel.DataAnnotations;

namespace ProAgil.API.Dtos
{
    public class UserDTO
    {
        [Required]
        [StringLength(50, MinimumLength=4)]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(30, MinimumLength=4)]
        public string Password { get; set; }
        [Required]
        public string FullName { get; set; }
    }
}

