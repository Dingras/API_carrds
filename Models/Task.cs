namespace API_carrds.Models
{
    public class Task
    {
        public int? id { get; set; }
        public Proyect proyect { get; set; }
        public string status { get; set; }
        public User responsible { get; set; }
        public DateTime created_at { get; set; } 
        public DateTime finished_at { get; set; }
        public DateTime time_limit { get; set;}
        public string info_text { get; set; }
        public string title { get; set;}
        public Spring spring { get; set; }

        public Task(int? id, Proyect proyect, string status, User responsible, DateTime created_at, DateTime finished_at, DateTime time_limit, string info_text, string title, Spring spring)
        {
            this.id = id;
            this.proyect = proyect;
            this.status = status;
            this.responsible = responsible;
            this.created_at = created_at;
            this.finished_at = finished_at;
            this.time_limit = time_limit;
            this.info_text = info_text;
            this.title = title;
            this.spring = spring;
        }
        public Task() { }
    }
}
