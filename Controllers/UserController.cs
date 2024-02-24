using API_carrds.DataControllers;
using API_carrds.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_carrds.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        Users users = new Users();
        
        [HttpGet]
        [Route("list")]
        public IEnumerable<User> ListUsers() 
        {
            return users.GetAll();
        }

        [HttpGet]
        [Route("user")]
        public User GetUser(int id)
        {
            return users.GetByID(id);  
        }

        [HttpGet]
        [Route("search/username")]
        public IActionResult SearchByUsername(string username)
        {
            User user = users.GetByUsername(username);
            if (user == null)
            {
                return Ok(new Error("No se encontro un usuario con ese username"));
            }
            else
            {
                return Ok(user);
            }
            
        }

        [HttpPost]
        [Route("add")]
        public string CreateUser(User u)
        {
            return users.Create(u); 
        }

        [HttpDelete]
        [Route("delete")]
        public string DeleteUser(int id)
        {
            return users.Delete(id);
        }

        [HttpPut]
        [Route("update")]
        public string UpdateUser(int id, User u)
        {
            return users.Update(id,u);
        }
    }
}
