using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anthR.Models.Seed
{
    public class anthRInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<anthR.Models.Core.anthRContext>
    {

        protected override void Seed(Core.anthRContext context)
        {
            //base.Seed(context);

            var masterSites = new List<Core.MasterSite>{
                new Core.MasterSite(){ Name = "Kingfisher" },
                new Core.MasterSite(){ Name = "Wilsons" }
            };

            context.SaveChanges();

        }

    }
}
