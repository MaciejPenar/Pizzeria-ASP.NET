using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PizzeriaOnline.Startup))]
namespace PizzeriaOnline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
