using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BRD_Music_Sem.Models;
using DataGate.Core;
using DataGate.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectArt.MVCPattern;

namespace BRD_Music_Sem
{
    public class Startup
    {
        private RouteHelper _routeHelper;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddRouting();
            services.AddDataGateServices();
            services.AddMvcPattern();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IControllerActivator controllerActivator, IActionActivator actionActivator,IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                //app.UseHsts();
            }
            
            DataGateORM.Connect(configuration.GetConnectionString("postgres"));
            TableRegistry();
            
            app.UseSession();

            _routeHelper = new RouteHelper(controllerActivator, actionActivator);
            var routeBuilder = new RouteBuilder(app);
            _routeHelper.Initialize(routeBuilder);
            app.UseStaticFiles();
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseRouter(routeBuilder.Build());
        }

        private void TableRegistry()
        {
            DataGateORM.Register<User>("users");
            DataGateORM.Register<Forum>("forum");
            DataGateORM.Register<TrackListModel>("tracklist");
        }
    }
}