using System.ComponentModel.DataAnnotations;

namespace PrefectVotingApplication.Models
{
    public class Votes
    {
        [Key]
        public int VoteId { get; set; }

        [Required(ErrorMessage = "Voter ID is required. ")]
        public int VoterId { get; set; } //= User UserId 
        [Required(ErrorMessage = "Voter is required. ")]
        public User UserId { get; set; }

        [Required(ErrorMessage = "Last Name is required. ")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Election ID is required. ")]
        public int ElectionId { get; set; }
        [Required(ErrorMessage = "Please input a date. ")]
        public DateTime Timestamp { get; set; }


    }
}
