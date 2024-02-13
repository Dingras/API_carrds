using API_carrds.DataControllers;
using API_carrds.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using System.Threading.Tasks;

namespace API_carrds.Controllers
{
    [ApiController]
    [Route("invited_to_proyects")]
    public class InvitedToProyectController
    {
        Invited_to_proyects itp = new Invited_to_proyects();

        [HttpPost]
        [Route("add")]
        public string CreateInvitedToProyect(InvitedToProyect i)
        {
            return itp.Create(i);
        }

        [HttpGet]
        [Route("list")]
        public IEnumerable<InvitedToProyect> GetAll() 
        {
            return itp.GetAll();
        }

        [HttpGet]
        [Route("invitedtoproyect")]
        public InvitedToProyect GetInvitedToProyect(int id)
        {
            return itp.GetByID(id);
        }

        [HttpPut]
        [Route("update")]
        public string UpdateInvitedToProyect(int id, InvitedToProyect i)
        {
            return itp.Update(id, i);
        }

        [HttpDelete]
        [Route("delete")]
        public string DeleteInvitedToProyect(int id)
        {
            return itp.Delete(id);
        }
    }
}
