namespace MaerskLine.CMS.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MaerskLine.CMS.Models.ApplicationDbContext>
    {
        private IList<string> _roleList = new List<string> { "APort","DPort", "Customer" };
        private IList<ItemCategory> _categories = new List<ItemCategory> { new ItemCategory { Name = "Food" }, new ItemCategory { Name = "Wearable" }, new ItemCategory { Name = "Machine" } };

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MaerskLine.CMS.Models.ApplicationDbContext context)
        {
            foreach (string role in this._roleList)
            {
                if (!context.Roles.Any(r => r.Name == role))
                {
                    new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context)).Create(new IdentityRole { Name = role });
                }
            }

            foreach (ItemCategory category in this._categories)
            {
                if (!context.ItemCategories.Any(x => x.Name == category.Name))
                {
                    context.ItemCategories.Add(category);
                }
            }

            context.SaveChanges();

            if (!context.Users.Any(u => u.UserName == "aport"))
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser { UserName = "aport", LockoutEnabled = false };

                manager.Create(user, "123123123");
                manager.AddToRole(user.Id, "APort");
            }

            if (!context.Users.Any(u => u.UserName == "dport"))
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser { UserName = "dport", LockoutEnabled = false };

                manager.Create(user, "123123123");
                manager.AddToRole(user.Id, "DPort");
            }
        }
    }
}
