using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace PrefectVotingApplication.Models
{
    public class AuditLog
    {
        [Key]
        public int LogId { get; set; }

        [Required]
        public int VoteId { get; set; } // foregin key to Votes

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
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
