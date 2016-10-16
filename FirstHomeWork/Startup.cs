using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FirstHomeWork.Startup))]
namespace FirstHomeWork
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
