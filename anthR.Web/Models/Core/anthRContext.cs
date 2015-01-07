using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anthR.Web.Models.Core
{
    public class anthRContext : DbContext
    {

        public DbSet<Todo.TodoItem> TodoItems { get; set; }
        public DbSet<Core.MasterSite> MasterSite { get; set; }
        public DbSet<Core.Project> Project { get; set; }
        public DbSet<Core.Staff> Staff { get; set; }
        public DbSet<Task.AnthRTask> AnthRTask { get; set; }
        public DbSet<Core.StaffOnTask> StaffOnTask { get; set; }
        public DbSet<Notes.Note> Note { get; set; }

        public anthRContext()
            : base("anthRContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }

    }
}
