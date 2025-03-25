using PrefectVotingApplication.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace PrefectVotingApplication.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Role Name is required. ")]
        [Display(Name = "Role Name")]
        public RoleNames RoleName { get; set; }

        [Required(ErrorMessage = "Choose from dropdown below...")]
        [Display(Name = "Vote Weight")]
        public int VoteWeight { get; set; } // Teachers get 20, students get 1

        // This is the navigation property for the reverse relationship.
        public ICollection<PrefectVotingApplicationUser> Users { get; set; } // This links back to all Users with this Role

        public enum RoleNames
        {
            Student,
            Teacher,
            Admin

        }
    }
}
