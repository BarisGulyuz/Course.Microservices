using FreeCourse.Services.Basket.Services;
using FreeCourse.Services.Basket.Settings;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;

namespace FreeCourse.Services.Basket
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FreeCourse.Services.Basket", Version = "v1" });
            });

            #region RedisService
            services.AddSingleton<RedisService>((sp) =>
            {
                RedisSettings redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;

                RedisService redisService = new RedisService(redisSettings.Host, redisSettings.Port);
                redisService.Connect();

                return redisService;
            });
            #endregion

            services.Configure<RedisSettings>(Configuration.GetSection("RedisSettings"));

            services.AddHttpContextAccessor();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IBasketService, BasketService>();

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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FreeCourse.Services.Basket v1"));
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
