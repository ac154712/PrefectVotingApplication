using System.ComponentModel.DataAnnotations;

namespace PrefectVotingApplication.Models
{
    public class Election
    {
        [Key]
        public int ElectionId { get; set; }

        [Required]
        public string ElectionTitle { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public ElectionStatus Status { get; set; }
        public ICollection<Votes> Votes { get; set; }

        //Enum for Election Status
        public enum ElectionStatus
        {
            Active, 
            Closed,
            Pending
        }
    }
}
