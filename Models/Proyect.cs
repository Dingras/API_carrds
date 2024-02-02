namespace API_carrds.Models
{
    public class Proyect
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public User created_by { get; set; }
        public DateTime created_at { get; set; }

        public Proyect() { }

        public Proyect(int? id, string name, string description, User created_by, DateTime created_at)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.created_by = created_by;
            this.created_at = created_at;
        }

        public Proyect (int id) { this.id = id; }
    }
}
