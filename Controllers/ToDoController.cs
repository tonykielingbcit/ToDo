using System.Reflection;
using WebAPI_2.Models;
using WebAPI_2.Models.Repository;

namespace WebAPI_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private ToDoRepo tr;

        public ToDoController(ToDoContext context)
        {
            tr = new ToDoRepo(context);
        }

        // GET: api/<ToDoController>
        [HttpGet]
        public ActionResult<List<ToDo>> Get()
        {
            var allTodos = tr.GetAllToDos();

            if (allTodos == null) return BadRequest("Something wrong getting ToDos. Try later please.");

            return Ok(allTodos);
        }

        [HttpGet("{id}")]
        public ActionResult<ToDo> Get(int id)
        {
            var oneTodo = tr.GetOneToDo(id);

            if (oneTodo == null) return NotFound($"Id {id} was not find.");

            return Ok(oneTodo);
        }

        [HttpPost]
        public ActionResult<ToDo> Post(ToDo toDo)
        {
            int tempId = toDo.Id;
            var addingTodo = tr.AddNewToDo(toDo);

            if (addingTodo.Id < 0) return BadRequest($"Id {tempId} already exists.");
            if (addingTodo == null) return BadRequest("Something wrong on DB. Try later, please.");

            return Ok(addingTodo);
        }

        [HttpPut]
        public ActionResult<ToDo> Put(ToDo toDo)
        {
            var tempId = toDo.Id;
            var updateTodo = tr.UpdateToDo(toDo);

            if (updateTodo == null) return BadRequest("Something bad on DB, please try again");
            if (updateTodo.Id < 0) return NotFound($"Id {tempId} was not find.");

            return Ok(updateTodo);
        }

        [HttpDelete("{id}")]
        public ActionResult<ToDo> Delete(int id)
        {
            var deleteTodo = tr.DeleteOneToDo(id);

            if (deleteTodo == null) return BadRequest("Something bad on DB, please try again");
            if (deleteTodo.Id < 0) return NotFound($"Id {id} was not find.");

            return Ok(deleteTodo);
        }
    }
}
