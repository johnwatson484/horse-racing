using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HorseRacing.Startup))]
namespace HorseRacing
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
