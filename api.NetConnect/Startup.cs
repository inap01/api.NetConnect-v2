﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("api.NetConnect", typeof(api.NetConnect.Startup))]

namespace api.NetConnect
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}