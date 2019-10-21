<<<<<<< HEAD
﻿using Microsoft.Owin;
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
=======
﻿using Microsoft.Owin;
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
>>>>>>> 961900145e26f7e80545174cdae75294aee15081
