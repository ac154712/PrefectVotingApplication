using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrefectVotingApplication.Models
{
    public class Votes
    {
        [Key]
        public int VoteId { get; set; }

        [ForeignKey("Voter")]
        [Required(ErrorMessage = "Voter ID is required. ")]
        [Display(Name = "Voter")]
        public int VoterId { get; set; }
        public User Voter { get; set; } //= User UserId 

        [ForeignKey("Receiver")]
        [Required(ErrorMessage = "Voter is required. ")]
        [Display(Name = "Receiver")]
        public int ReceiverId { get; set; }
        public User Receiver { get; set; }

        [Required(ErrorMessage = "Election ID is required. ")]
        public int ElectionId { get; set; }
        [Required(ErrorMessage = "Please input a date. ")]
        public DateTime Timestamp { get; set; }


    }
}
