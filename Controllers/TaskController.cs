using API_carrds.DataControllers;
using Microsoft.AspNetCore.Mvc;
using Task = API_carrds.Models.Task;

namespace API_carrds.Controllers
{
    [ApiController]
    [Route("tasks")]
    public class TaskController
    {
        Tasks tasks = new Tasks();

        [HttpPost]
        [Route("add")]
        public string CreateTask(Task t)
        {
            return tasks.Create(t);
        }

        [HttpGet]
        [Route("list")]
        public IEnumerable<Task> GetAllTask()
        {
            return tasks.GetAll();
        }

        [HttpGet]
        [Route("spring")]
        public IEnumerable<Task> GetBySpring(int id_spring)
        {
            return tasks.GetBySpring(id_spring);
        }

        [HttpGet]
        [Route("task")]
        public Task GetTask(int id)
        {
            return tasks.GetByID(id);
        }

        [HttpPut]
        [Route("update")]
        public string UpdateTask(int id, Task t)
        {
            return tasks.Update(id, t);
        }

        [HttpPatch]
        [Route("update/spring")]
        public string UpdateSpringToTask(int id, int id_spring)
        {
            return tasks.UpdateSpringToTask(id, id_spring);
        }

        [HttpDelete]
        [Route("delete")]
        public string DeleteTask(int id)
        {
            return tasks.Delete(id);
        }
    }
}
