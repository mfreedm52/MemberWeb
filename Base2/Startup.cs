using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Base2.Startup))]
namespace Base2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
