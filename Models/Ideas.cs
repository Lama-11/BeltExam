using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace BeltExam.Models
{
    public class Ideas
    {
        [Key]
        public int IdeaId { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Must be at least 5 characters")]
        public string Idea {get; set;}
        public int UserId {get; set;}

        public User PostedBy { get; set; }        
        public List<Like> Likes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}