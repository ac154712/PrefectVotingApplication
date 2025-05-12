using PrefectVotingApplication.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace PrefectVotingApplication.Models
{
    public class Role //i needed to make my own custom Role model instead of the Aspnet one already generated because i need extra properties like VoteWeight which is not-auth. Its better to seperate application logic roles(non-auth) to identity(auth) roles for overall flexibility, security and just clarity.
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
        public ICollection<PrefectVotingApplicationUser> Users { get; set; } // This links baje ck to all Users with this Role

        public enum RoleNames
        {
            Student,
            Teacher,
            Staff

        }
    }
}
