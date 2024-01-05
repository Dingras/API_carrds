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
            return users.ListUsers();
        }
        [HttpPost]
        [Route("add")]
        public string CreateUser(User u)
        {
            return users.Create(u); 
        }
    }
}
