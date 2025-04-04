using PrefectVotingApplication.Areas.Identity.Data;
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
        public string VoterId { get; set; }
        public PrefectVotingApplicationUser Voter { get; set; } //references the id from users table and stores it as Voter 

        [ForeignKey("Receiver")]
        [Required(ErrorMessage = "Voter is required. ")]
        [Display(Name = "Receiver")]
        public string ReceiverId { get; set; }
        public PrefectVotingApplicationUser Receiver { get; set; } //references the id from users table and stores it as Receiver

        [Required(ErrorMessage = "Election ID is required. ")]
        public int ElectionId { get; set; }
        [Required(ErrorMessage = "Please input a date. ")]
        public DateTime Timestamp { get; set; }


    }
}
