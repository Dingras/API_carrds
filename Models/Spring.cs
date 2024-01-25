namespace API_carrds.Models
{
    public class Spring
    {
        public int? id { get; set; }
        public Proyect proyect { get; set; }
        public string title { get; set; }
        public string description { get; set; }

        public Spring() { } 

        public Spring(int? id, Proyect proyect, string title, string description)
        {
            this.id = id;
            this.proyect = proyect;
            this.title = title;
            this.description = description;
        }
    }
}
