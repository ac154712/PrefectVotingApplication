using System.ComponentModel.DataAnnotations;

namespace PrefectVotingApplication.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }

        [Required]
        public int VoteWeight { get; set; } // Teachers get 20, students get 1

        public ICollection<User> Users { get; set; } 
    }
}
