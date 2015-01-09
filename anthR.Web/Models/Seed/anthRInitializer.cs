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
                new Core.MasterSite(){ Name = "SMA" },
                new Core.MasterSite(){ Name = "Aston Barclay" },
                new Core.MasterSite(){ Name = "Pioneer" },
                new Core.MasterSite(){ Name = "CVA Auctions" },
                new Core.MasterSite(){ Name = "Kingfisher" },
                new Core.MasterSite(){ Name = "Fleet Auction Group" },
                new Core.MasterSite(){ Name = "Central Car Auctions" },
                new Core.MasterSite(){ Name = "Newport Motor Auctions" },
                new Core.MasterSite(){ Name = "Shotts Motor Auctions" },
                new Core.MasterSite(){ Name = "Bawtry Motor Auctions" },
                new Core.MasterSite(){ Name = "Eastbourne Car Auctions" },
                new Core.MasterSite(){ Name = "Morris Leslie Group" },
                new Core.MasterSite(){ Name = "Shoreham" },
                new Core.MasterSite(){ Name = "City Auction Group" },
                new Core.MasterSite(){ Name = "South Western Vehicle Auctions" },
                new Core.MasterSite(){ Name = "Jet Logistics" },
                new Core.MasterSite(){ Name = "Protruck Auctions" },
                new Core.MasterSite(){ Name = "ThirtyDegreesWest" },
                new Core.MasterSite(){ Name = "Malcolm Harrison" },
                new Core.MasterSite(){ Name = "SMD" },
                new Core.MasterSite(){ Name = "European Vehicle Sales" },
                new Core.MasterSite(){ Name = "West Coast Motor Auctions" },
                new Core.MasterSite(){ Name = "Independent Motor Auctions" },
                new Core.MasterSite(){ Name = "Merlin Group" },
                new Core.MasterSite(){ Name = "Circle Auctions" },
                new Core.MasterSite(){ Name = "RiHago" },
                new Core.MasterSite(){ Name = "UK Auctions" },
                new Core.MasterSite(){ Name = "Swansea Motor Auctions" }
            };

            masterSites.ForEach(ms => context.MasterSite.Add(ms));
            context.SaveChanges();

            var statuses = new List<Core.Status>{
                new Core.Status() { Description = "Awaiting Start", HexColor = "#CCCCCC" },
                new Core.Status() { Description = "In-Progress", HexColor = "#00FF00" },
                new Core.Status() { Description = "Waiting", HexColor = "#00FF00" },
                new Core.Status() { Description = "Complete", HexColor = "#00FF00" },
                new Core.Status() { Description = "Delegated", HexColor = "#00FF00" }
            };

            statuses.ForEach(s => context.Status.Add(s));
            context.SaveChanges();

            var staff = new List<Core.Staff>{
                new Core.Staff(){ Name = "Tim", Email = "tim@kingfisher-systems.co.uk" },
                new Core.Staff(){ Name = "Simon", Email = "Simon@kingfisher-systems.co.uk" },
                new Core.Staff(){ Name = "Munnuwar", Email = "Munnuwar@kingfisher-systems.co.uk" },
                new Core.Staff(){ Name = "David", Email = "David@kingfisher-systems.co.uk" },
                new Core.Staff(){ Name = "Graeme", Email = "Graeme@kingfisher-systems.co.uk" },
                new Core.Staff(){ Name = "Ian", Email = "Ian@kingfisher-systems.co.uk" },
                new Core.Staff(){ Name = "Paul", Email = "Paul@kingfisher-systems.co.uk" },
                new Core.Staff(){ Name = "Andrew", Email = "Andrew@kingfisher-systems.co.uk" },
                new Core.Staff(){ Name = "Rosemary", Email = "Rosemary@kingfisher-systems.co.uk" }
            };

            staff.ForEach(s => context.Staff.Add(s));
            context.SaveChanges();

        }

    }
}
