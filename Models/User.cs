using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeltExam.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z ]+$",ErrorMessage="Name only contains letters and space")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$",ErrorMessage="Alias only contains letters and numbers")]
        public string Alias { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name ="Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]        
        public string Password { get; set; }

        [Required]        
        [Display(Name ="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        public string Confirm { get; set; }

        public List<Ideas> Idea { get; set; }
        public List<Like> Likes { get; set; }      
    }  

}