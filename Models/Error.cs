namespace API_carrds.Models
{
    public class Error
    {
        public string message { get; }

        public Error(string message) { this.message = message; }
    }
}
