using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel.AspNetCore.OAuth2Introspection;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore( options => 
            {
                // require scope1 or scope2
                //var policy = ScopePolicy.Create("scope1", "scope2");
                //options.Filters.Add(new AuthorizeFilter(policy));

            }).AddAuthorization()
                .AddJsonFormatters();
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = "http://localhost:5000";
                options.SupportedTokens = SupportedTokens.Both;

             
                options.RequireHttpsMetadata = false;
                options.ApiName = "api1";
                options.ApiSecret = "{2ACAFD66-F33A-4561-9D80-0C73A0D3D318}";
                //options.EnableCaching = true;
                //options.CacheDuration = TimeSpan.FromMinutes(10); // that's the default
            });

           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            


            app.UseMvc();
        }
    }
}
