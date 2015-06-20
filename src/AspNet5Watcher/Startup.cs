using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet5Watcher.SearchEngine;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Runtime;

namespace AspNet5Watcher
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            _configuration = new ConfigurationBuilder(appEnv.ApplicationBasePath)
             .AddEnvironmentVariables()
             .AddJsonFile("config.json")
             .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<SearchRepository, SearchRepository>();

            services.AddInstance(_configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();       
            app.UseMvc();         
        }
    }
}
