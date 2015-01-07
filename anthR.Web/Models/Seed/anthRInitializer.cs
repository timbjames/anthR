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

            var masterSites = new List<Core.MasterSite>{
                new Core.MasterSite(){ Name = "Wilsons" },
                new Core.MasterSite(){ Name = "SMA" }
            };

            masterSites.ForEach(ms => context.MasterSite.Add(ms));
            context.SaveChanges();

        }

    }
}
