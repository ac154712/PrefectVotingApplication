using System.ComponentModel.DataAnnotations;

namespace PrefectVotingApplication.Models
{
    public class Votes
    {
        [Key]
        public int VoteId { get; set; }

        [Required]
        public int VoterId { get; set; }
        public User Voter { get; set; }



    }
}
