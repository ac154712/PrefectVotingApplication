using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PrefectVotingApplication.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "First Name is required. ")]
        [StringLength(50, ErrorMessage = "First Name must not exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required. ")]
        [StringLength(50, ErrorMessage = "Last Name must not exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required. ")]
        [EmailAddress(ErrorMessage = "Invalid email format. ")]
        public string Email { get; set; }

        [Required]
        public string Picture { get; set; }

        [StringLength(450, ErrorMessage = "Description must not exceed 600 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Role ID is required. ")]
        public int RoleId { get; set; }
        public Role Role { get; set; } //Navigation property
    }
}
