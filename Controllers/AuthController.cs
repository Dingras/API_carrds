using API_carrds.DataControllers;
using API_carrds.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_carrds.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        Users users = new Users();

        [HttpPost]
        [Route("login")]
        public User? LogIn(User u)
        {
            return users.AuthUser(u);
        }

    }
}
