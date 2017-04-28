using DopeZoo.Migrations;
using DopeZoo.Models;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(DopeZoo.Startup))]

namespace DopeZoo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ShopDbContext, Configuration>());

            ConfigureAuth(app);
        }
    }
}