namespace API_carrds.Models
{
    public class InvitedToProyect
    {
        public int id { get; set; }
        public Proyect proyect { get; set; }
        public User user { get; set; }
        public string status { get; set; }

        public InvitedToProyect() { }

        public InvitedToProyect(int id, Proyect proyect, User user, string status)
        {
            this.id = id;
            this.proyect = proyect;
            this.user = user;
            this.status = status;
        }
    }
}
