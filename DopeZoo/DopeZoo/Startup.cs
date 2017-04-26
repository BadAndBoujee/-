using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DopeZoo.Startup))]
namespace DopeZoo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
