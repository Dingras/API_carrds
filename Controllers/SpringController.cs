using API_carrds.DataControllers;
using API_carrds.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_carrds.Controllers
{   
    [ApiController]
    [Route("springs")]
    public class SpringController: ControllerBase
    {
        Springs springs = new Springs();

        [HttpGet]
        [Route("list")]
        public IEnumerable<Spring> ListSprings()
        {
            return springs.GetAll();
        }

        [HttpGet]
        [Route("proyect")]
        public IEnumerable<Spring> ListSpringsByProyect(int id_proyect)
        {
            return springs.GetByProyect(id_proyect);
        }

        [HttpGet]
        [Route("spring")]
        public Spring GetSpring(int id)
        {
            return springs.GetByID(id);
        }

        [HttpPost]
        [Route("add")]
        public string CreateSpring(Spring s)
        {
            return springs.Create(s);
        }

        [HttpDelete]
        [Route("delete")]
        public string DeleteSpring(int id)
        {
            return springs.Delete(id);
        }

        [HttpPut]
        [Route("update")]
        public string UpdateSpring(int id, Spring s)
        {
            return springs.Update(id, s);
        }
    }
}
