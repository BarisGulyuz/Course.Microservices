using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace FreeCourse.Gateways
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOcelot();

            services.AddAuthentication().AddJwtBearer("GatewayAuthScheme", (opt) =>
             {
                 opt.Authority = Configuration.GetSection("IdentityServerUri").Value;
                 opt.Audience = Configuration.GetSection("ResourceName").Value; ;
                 //opt.RequireHttpsMetadata = false;
             });

        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            await app.UseOcelot();


        }
    }
}
