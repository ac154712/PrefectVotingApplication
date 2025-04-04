using Microsoft.Build.Framework;
using Microsoft.Identity.Client;
using System.Runtime.InteropServices;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PrefectVotingApplication.Areas.Identity.Data;
namespace PrefectVotingApplication.Models
{
    public class AuditLog
    {
        [Key]
        public int LogId { get; set; }

        [Required(ErrorMessage = "Vote ID is required. ")]
        public int VoteId { get; set; } // foregin key to Votes
        public Votes Vote { get; set; }

        [Required(ErrorMessage = "User ID is required. ")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public PrefectVotingApplicationUser User { get; set; }

        [Required(ErrorMessage = "Please input an action. ")]
        public AuditAction Action { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        //enum for Audit Actions
        public enum AuditAction
        {
            Created, 
            Updated, 
            Deleted
        }
    }
}
