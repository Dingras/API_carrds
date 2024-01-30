namespace API_carrds.Models
{
    /// <summary>
    /// Esta clase contiene las propiedades para un Usuario
    /// </summary>
    public class User
    {
        /// <summary>
        /// Identificador unico de Usuario
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// Nombre de la cuenta del Usuario
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// Clave de privada para ingreso a la cuenta del Usuario
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// Nombre del propietario de la cuenta de Usuario
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Apellido del propietario de la cuenta de Usuario
        /// </summary>
        public string last_name { get; set; }
        /// <summary>
        /// Direccion de correo electronico del Usuario
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// Direccion URL de avatar del Usuario
        /// </summary>
        public string avatar_url { get; set; }

        public User(string username, string password, string name, string last_name, string email, string avatar_url)
        {
            this.username = username;
            this.password = password;
            this.name = name;
            this.last_name = last_name;
            this.email = email;
            this.avatar_url = avatar_url;
        }
        public User(int id, string username, string password, string name, string last_name, string email,)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.name = name;
            this.last_name = last_name;
            this.email = email;
            this.avatar_url = avatar_url;
        }
        public User() { }
    }
}
