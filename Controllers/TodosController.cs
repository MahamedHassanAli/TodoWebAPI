using Microsoft.AspNetCore.Mvc;
using TodoWebAPI.Models;
using System.Collections.Generic;
using System.Data.Common; // Legg til denne for å bruke List<Todo>

namespace TodoWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        public static List<Todo> Todos = new List<Todo>
        {
            new Todo { Id = 1, Title = "Buy groceries", Description = "Get milk, bread, and eggs from the store", IsCompleted = false },
            new Todo { Id = 2, Title = "Finish project report", Description = "Complete the final section and review formatting", IsCompleted = true },
            new Todo { Id = 3, Title = "Book dentist appointment", Description = "Schedule a check-up for next month", IsCompleted = false },
            new Todo { Id = 4, Title = "Read a new book", Description = "Start reading 'The Great Gatsby'", IsCompleted = false },
            new Todo { Id = 5, Title = "Clean the garage", Description = "Organize tools and throw out old items", IsCompleted = true },
            new Todo { Id = 6, Title = "Plan summer vacation", Description = "Research destinations and book flights", IsCompleted = false },
        };

        // GET: api/todos
        [HttpGet]
        public ActionResult<IEnumerable<Todo>> GetTodos()
        {
            return Ok(Todos);
        }

        // GET api/todos/{Id} skal retunere todo med en gitt id
        [HttpGet("{id}")]
        public ActionResult<Todo> GetTodo(int id)
        {
            var todo = Todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                NotFound();
            }
            return Ok(todo);
        }
        [HttpPost]
        public ActionResult<Todo> CreateTodo(Todo newTodo)
        {
            newTodo.Id = Todos.Max(t => t.Id) + 1;
            Todos.Add(newTodo);
            return CreatedAtAction(nameof(GetTodo), new { id = newTodo.Id }, newTodo);
        }
        // PUT: api/todos/{id}

        [HttpPut("{id}")]
        public ActionResult UpdateTodo(int id, Todo updatedTodo)
        {
            var existingTodo = Todos.FirstOrDefault(t => t.Id == id);
            if (existingTodo == null)
            {
                return NotFound();
            }
            existingTodo.Title = updatedTodo.Title;
            existingTodo.Description = updatedTodo.Description;
            existingTodo.IsCompleted = updatedTodo.IsCompleted;

            return NoContent();
        }

        [HttpDelete("id")]
        public ActionResult DeleteTodo(int id)
        {
            var todo = Todos.FirstOrDefault(t => t.Id == id);
            if(todo == null) 
            { 
               return  NotFound();
            }

            Todos.Remove(todo);
            return NoContent();
        }
    }
}

