using System.ComponentModel.DataAnnotations;

namespace PrefectVotingApplication.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Role Name is required. ")]
        public RoleNames RoleName { get; set; }

        [Required(ErrorMessage = "Choose from dropdown below...")]
        public int VoteWeight { get; set; } // Teachers get 20, students get 1

        public ICollection<User> Users { get; set; } 

        public enum RoleNames
        {
            Student,
            Teacher,
            Admin

        }
    }
}
