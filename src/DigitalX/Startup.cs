using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DigitalX.Startup))]
namespace DigitalX
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
