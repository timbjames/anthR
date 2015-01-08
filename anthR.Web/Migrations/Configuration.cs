namespace anthR.Web.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<anthR.Web.Models.Core.anthRContext>
    {
        
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "anthR.Web.Models.Core.anthRContext";
        }

        protected override void Seed(anthR.Web.Models.Core.anthRContext context)
        {
            
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            // this will add duplicate data all the time :(
            //var status = new List<anthR.Web.Models.Core.Status>{
            //    new anthR.Web.Models.Core.Status(){ Description = "Awaiting", HexColor = "#CCCCCC" },
            //    new anthR.Web.Models.Core.Status(){ Description = "Inprogress", HexColor = "#00FF00" }
            //};

            //status.ForEach(s => context.Status.Add(s));

            context.Status.AddOrUpdate(s => s.Description,
                new anthR.Web.Models.Core.Status() { Description = "Awaiting Start", HexColor = "#CCCCCC" },
                new anthR.Web.Models.Core.Status() { Description = "In-Progress", HexColor = "#00FF00" },
                new anthR.Web.Models.Core.Status() { Description = "Waiting", HexColor = "#00FF00" },
                new anthR.Web.Models.Core.Status() { Description = "Complete", HexColor = "#00FF00" },
                new anthR.Web.Models.Core.Status() { Description = "Delegated", HexColor = "#00FF00" }
            );

        }

    }
}
