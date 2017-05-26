using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewTechTask.Startup))]
namespace NewTechTask
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
