namespace MeetupAPI.Entities
{
    public class Meetup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Organizer { get; set; }
        public DateTime Date { get; set; }
        public bool IsPrivate { get; set; }
        
        public virtual Location Location { get; set; }

        public virtual ICollection<Lecture> Lectures { get; set; }
        public int? CreatedById { get; set; }
        public User CreatedBy { get; set; }
    }
}
