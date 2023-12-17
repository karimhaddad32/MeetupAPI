using MeetupAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace MeetupAPI.DTOs
{
    public class FullMeetupDto
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        public string Organizer { get; set; }
        public DateTime Date { get; set; }
        public bool IsPrivate { get; set; }
        public LocationDto Location { get; set; }   
        public List<LectureDto> Lectures { get; set; }
    }
}
