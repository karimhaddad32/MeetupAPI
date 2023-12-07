namespace MeetupAPI.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }    
        public int MeetupId { get; set; }
    }
}
