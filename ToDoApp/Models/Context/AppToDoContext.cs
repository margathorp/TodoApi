using Microsoft.EntityFrameworkCore;

namespace ToDoApp.Models.Context
{
    public class AppToDoContext : DbContext
    {
        
        public AppToDoContext(DbContextOptions options) : base(options)
        {
            
        }
        public virtual DbSet<Todo> ToDos {get;set;}
    }
}