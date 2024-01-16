namespace MeetupAPI.Controllers
{
    public class MeetupQuery
    {
        public string? SearchPhrase { get; set; } 
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}