using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;


namespace WebApplication1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //REGISTER DEPENDENT TYPES (SERVICES) WITH IOC CONTAINERS HERE
            services.AddSingleton<ILog, ConsoleLog>();
            services.Add(new ServiceDescriptor(typeof(ILog), new ConsoleLog()));        //registers as singleton -- only one instance is created 

            services.AddTransient<ILog, ConsoleLog>();
            services.Add(new ServiceDescriptor(typeof(ILog), new ConsoleLog()));        //registers as transient -- creates a new instance upon each time its asked

            services.AddScoped<ILog, ConsoleLog>();
            services.Add(new ServiceDescriptor(typeof(ILog), new ConsoleLog()));        //registers as scoped    -- instance is created and used once per request

        }

        //configure with logger
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/mylog-{Date}.txt");

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Content")),
                RequestPath = new PathString("/Admin")
            });

            app.Run(MyMiddleware);
        }

        private async Task MyMiddleware(HttpContext context)
        {
            await context.Response.WriteAsync("2nd middleware");
        }
    }
}
