using System.Security;

namespace API_carrds.DataControllers.Interfaces
{
    public interface Icrud<T>
    {
        // Create
        public string Create(T t);
        //Read
        public IEnumerable<T> ListUsers();
        public T ListUser(int id);
        // Update
        public string Update(int id,T t);
        //Delete
        public string Delete(int id);
    }
}
