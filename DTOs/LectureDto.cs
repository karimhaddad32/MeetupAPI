using System.ComponentModel.DataAnnotations;

namespace MeetupAPI.DTOs
{
    public class LectureDto
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MinLength(5)]
        public string Author { get; set; }

        [Required]
        [MinLength(5)]
        public string Topic { get; set; }

        public string Description { get; set; }
    }
}