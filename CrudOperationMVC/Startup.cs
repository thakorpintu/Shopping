using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrudOperationMVC.Startup))]
namespace CrudOperationMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
