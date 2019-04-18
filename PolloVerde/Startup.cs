using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PolloVerde.Startup))]
namespace PolloVerde
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
