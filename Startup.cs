using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CSharp_Scraper.Startup))]
namespace CSharp_Scraper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
