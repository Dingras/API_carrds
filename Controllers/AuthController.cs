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
        public IActionResult LogIn(User u)
        {
            User? user = users.AuthUser(u);
            if (user == null)
            {
                return Ok(new Error("Usuario o Contraseña incorrectos."));
            }
            return Ok(user);
        }

    }
}
