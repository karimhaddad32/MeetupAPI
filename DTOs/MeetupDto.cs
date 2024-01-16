using System.ComponentModel.DataAnnotations;

namespace MeetupAPI.DTOs
{
    public class MeetupDto
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        public string Organizer { get; set; }
        public DateTime Date { get; set; }
        public bool IsPrivate { get; set; }
    }
}
