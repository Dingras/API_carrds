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

        [HttpDelete]
        [Route("delete")]
        public string DeleteTask(int id)
        {
            return tasks.Delete(id);
        }
    }
}
