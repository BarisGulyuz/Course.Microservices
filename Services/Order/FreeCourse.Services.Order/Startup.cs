using FreeCourse.Services.Order.Application.Command;
using FreeCourse.Services.Order.Infrastructre;
using FreeCourse.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;

namespace FreeCourse.Services.Order
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

            AuthorizationPolicy requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            services.AddControllers(opt => { opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy)); });
            //services.AddControllers();

            services.AddDbContext<OrderDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("OrderDb"), configure =>
                {
                    configure.MigrationsAssembly("FreeCourse.Services.Order.Infrastructre");
                });

            });

            services.AddHttpContextAccessor();
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddMediatR(typeof(CreateOrderCommand).Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FreeCourse.Services.Order", Version = "v1" });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer((opt) =>
            {
                opt.Authority = Configuration.GetSection("IdentityServerUri").Value;
                opt.Audience = Configuration.GetSection("ResourceName").Value; ;
                //opt.RequireHttpsMetadata = false;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FreeCourse.Services.Order v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
