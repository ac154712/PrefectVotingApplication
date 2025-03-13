using System.ComponentModel.DataAnnotations;

namespace PrefectVotingApplication.Models
{
    public class Election
    {
        [Key]
        public int ElectionId { get; set; }

        [Required(ErrorMessage = "Title is required. ")]
        [StringLength(100, ErrorMessage = "Election Name cannot exceed 100 characters.")]
        [Display(Name = "Election Name")]
        public string ElectionTitle { get; set; }

        [Required(ErrorMessage = "Start Date is required. ")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required. ")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Status is required. ")]
        [EnumDataType(typeof(ElectionStatus), ErrorMessage = "Invalid status.")]
        public ElectionStatus Status { get; set; }
        public ICollection<Votes> Votes { get; set; } //Linking to Votes model - collecting vote ids

        //Enum for Election Status
        public enum ElectionStatus
        {
            Active, 
            Closed,
            Pending
        }
    }
}
