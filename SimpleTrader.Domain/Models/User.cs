namespace SimpleTrader.Domain.Models
{
    public class User : DomainObject
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
