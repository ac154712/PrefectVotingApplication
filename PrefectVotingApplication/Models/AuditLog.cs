using Microsoft.Build.Framework;
using Microsoft.Identity.Client;
using System.Runtime.InteropServices;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
namespace PrefectVotingApplication.Models
{
    public class AuditLog
    {
        [Key]
        public int LogId { get; set; }

        [Required(ErrorMessage = "Vote ID is required. ")]
        public int VoteId { get; set; } // foregin key to Votes

        [Required(ErrorMessage = "User ID is required. ")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "Please input an action. ")]
        public AuditAction Action { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.Now;

        //enum for Audit Actions
        public enum AuditAction
        {
            Created, 
            Updated, 
            Deleted
        }
    }
}
