using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anthR.Web.Models.Seed
{
    public class anthRInitializer : System.Data.Entity.DropCreateDatabaseAlways<Core.anthRContext>
    {
       
        protected override void Seed(Core.anthRContext context)
        {

            var statuses = new List<Core.Status>{
                new Core.Status() { Description = "Awaiting Start", HexColor = "#CCCCCC" },
                new Core.Status() { Description = "In-Progress", HexColor = "#00FF00" },
                new Core.Status() { Description = "Waiting", HexColor = "#00FF00" },
                new Core.Status() { Description = "Complete", HexColor = "#00FF00" },
                new Core.Status() { Description = "Delegated", HexColor = "#00FF00" }
            };

            statuses.ForEach(s => context.Status.Add(s));
            context.SaveChanges();

        }

    }
}
