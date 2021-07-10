
using System.ComponentModel.DataAnnotations;


namespace BeltExam.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        public int UserId { get; set; }
        public User Users { get; set; }
         public int IdeaId { get; set; }
        public Ideas Idea { get; set; }
    }
}