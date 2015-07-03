using AspNet5Watcher.Hubs;
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
            services.AddSignalR(options =>
            {
                options.Hubs.EnableDetailedErrors = true;
            });

            services.AddScoped<SearchRepository, SearchRepository>();
            services.AddInstance(_configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();       
            app.UseMvc();
            app.UseSignalR();
        }
    }
}
