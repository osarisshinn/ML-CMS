using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MaerskLine.CMS.Startup))]
namespace MaerskLine.CMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
