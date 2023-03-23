using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Management_Debt.Startup))]
namespace Management_Debt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
