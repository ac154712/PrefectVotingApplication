using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PrefectVotingApplication.Models;

namespace PrefectVotingApplication.Areas.Identity.Data;

// Add profile data for application users by adding properties to the PrefectVotingApplicationUser class
public class PrefectVotingApplicationUser : IdentityUser
{
    [Required(ErrorMessage = "First Name is required. ")]
    [StringLength(50, ErrorMessage = "First Name must not exceed 50 characters.")]
    [Display(Name = "First Name")]  //display this on front end 
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required. ")]
    [StringLength(50, ErrorMessage = "Last Name must not exceed 50 characters.")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required. ")]
    [EmailAddress(ErrorMessage = "Invalid email format. ")]
    public string Email { get; set; }

    public string ImagePath { get; set; } // store file path instead of binary data in database

    [StringLength(450, ErrorMessage = "Description must not exceed 600 characters.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Role ID is required. ")]
    public int RoleId { get; set; }
    // This is the navigation property.EF will use the RoleId foreign key to join the Role table.
    public Role Role { get; set; } //Navigation property
}

