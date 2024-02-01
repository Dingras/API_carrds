using API_carrds.DataControllers;
using Microsoft.AspNetCore.Mvc;


namespace API_carrds.Controllers
{
    [ApiController]
    [Route("task")]
    public class TaskController
    {
        Tasks task = new Tasks();

        [HttpPost]
        [Route("add")]
        public string CreateTask(API_carrds.Models.Task t)
        {
            return task.Create(t);
        }

        [HttpGet]
        [Route("List")]
        public IEnumerable<API_carrds.Models.Task> GetAllTask()
        {
            return task.GetAll();
        }

        [HttpGet]
        [Route("listByID")]
        public API_carrds.Models.Task GetTask(int id)
        {
            return task.GetByID(id);
        }
        [HttpPut]
        [Route("update")]
        public string UpdateTask(int id, API_carrds.Models.Task t)
        {
            return task.Update(id, t);
        }

        [HttpDelete]
        [Route("delete")]
        public string DeleteTask(int id)
        {
            return task.Delete(id);
        }
    }
}
