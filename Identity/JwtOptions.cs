namespace MeetupAPI.Identity
{
    public class JwtOptions
    {
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public int JwtExpireDay { get; set; }
    }
}
