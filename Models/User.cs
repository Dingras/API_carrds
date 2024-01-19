namespace API_carrds.Models
{
    public class User
    {
        public int? id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }

        public User(string username, string password, string name, string last_name, string email)
        {
            this.username = username;
            this.password = password;
            this.name = name;
            this.last_name = last_name;
            this.email = email;
        }
        public User(int id, string username, string password, string name, string last_name, string email)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.name = name;
            this.last_name = last_name;
            this.email = email;
        }
        public User() { }
    }
}
