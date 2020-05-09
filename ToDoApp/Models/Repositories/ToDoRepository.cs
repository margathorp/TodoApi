using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Models.Context;

namespace ToDoApp.Models.Repositories
{
    public class ToDoRepository
    {
        private readonly AppToDoContext _appToDoContext;

        public ToDoRepository(AppToDoContext appToDoContext)
        {
            _appToDoContext = appToDoContext;
        }

        public async Task<Todo> GetById(Guid id)
        {
            return await _appToDoContext.ToDos.FindAsync(id);
        }

        public async Task<IList<Todo>> GetAll()
        {
            return await _appToDoContext.ToDos.ToListAsync();
        }

        public async Task<Todo> Create(ToDoDto dto)
        {
            var todo = new Todo(dto);            
            _appToDoContext.ToDos.Add(todo);
            await _appToDoContext.SaveChangesAsync();
            return todo;
        }

        public async Task Edit(Guid id, ToDoDto dto)
        {
            var todo = new Todo(id, dto.Nome, dto.Descricao, dto.Concluido);
            await Edit(todo);
        }

        public async Task Edit(Todo entity)
        {
            _appToDoContext.Entry<Todo>(entity).State = EntityState.Modified;
            await _appToDoContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var todo = _appToDoContext.ToDos.Find(id);
            if(todo == null)
                throw new KeyNotFoundException("Tarefa não encontrada");
            
            _appToDoContext.ToDos.Remove(todo);
            await _appToDoContext.SaveChangesAsync();
        }

        public async Task Concluir(Guid id)
        {
            var todo = await GetById(id);
            todo.Concluido = true;
            await Edit(todo);
        }
    }
}