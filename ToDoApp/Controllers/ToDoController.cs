using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;
using ToDoApp.Models.Context;
using ToDoApp.Models.Repositories;

namespace ToDoApp.Controllers
{
    [ApiController]
    [Route("api/todo")]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoRepository _toDoRepository;
        public ToDoController(ToDoRepository appToDoContext)
        {
            _toDoRepository = appToDoContext;
        }
        
        [HttpPost]
        public async Task<IActionResult> CriarTodo([FromBody] ToDoDto entity)
        {
            var todo = await _toDoRepository.Create(entity);
            return Ok(new MessageDto{Mensagem = $"A Tarefa {todo.Id} foi criada com sucesso!"});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTodo([FromRoute]Guid id,  [FromBody] ToDoDto entity)
        {
            return Ok(_toDoRepository.Edit(id, entity));
        }

        [HttpPut("{id}/concluir")]
        public async Task<IActionResult> ConcluirToDo([FromRoute]Guid id)
        {
            await _toDoRepository.Concluir(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var todo = await _toDoRepository.GetById(id);
            if(todo == null)
                return BadRequest("Tarefa não encontrada");
            return Ok(todo);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _toDoRepository.GetAll());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirTodo([FromRoute]Guid id)
        {
            try
            {
                await _toDoRepository.Delete(id);
                return Ok(new MessageDto{Mensagem = $"A tarefa {id}, foi exluída com sucesso!", Sucesso = true});
            }
            catch(KeyNotFoundException nf)
            {
                return NotFound(new MessageDto{Mensagem = nf.Message, Sucesso = false});
            }
            catch(Exception e)
            {
                return BadRequest(new MessageDto{Mensagem = e.Message, Sucesso = false});
            }
            
        }
    }
}