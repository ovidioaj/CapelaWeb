﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Capela.Web.Startup))]
namespace Capela.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
