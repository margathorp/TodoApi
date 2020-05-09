using System;

namespace ToDoApp.Models
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Concluido { get; set; }

        public Todo()
        {
            Id = Guid.NewGuid();
        }

        public Todo(Guid id, string nome, string descricao, bool concluido)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Concluido = concluido;
        }

        public Todo(ToDoDto entity)
        {
            Id = Guid.NewGuid();
            Nome = entity.Nome;
            Descricao = entity.Descricao;
            Concluido = entity.Concluido;
        }
    }
}