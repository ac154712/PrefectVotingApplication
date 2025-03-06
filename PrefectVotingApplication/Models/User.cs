using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PrefectVotingApplication.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Picture { get; set; }

        [Required]
        public string Description { get; set; }

        public int RoleId { get; set; }
        public RoleManager Role { get; set; } //Navigation property
    }
}
