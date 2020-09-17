using Koai.WebApi.Configurations.Response;
using Koai.WebApi.Configurations.Swagger;
using Koai.WebApi.MultiTenancy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Koai.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(opt => opt.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer())));
            services.AddSwaggerGen(opt => opt.OperationFilter<TenantHeaderAttrFilter>());

            services.AddMultiTenant<Tenant, int>()
                .WithProvider<KoaiTenantProvider>()
                .WithHeaderAttributeStrategy("x-tenant");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Koai Multi-Tenancy V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMultiTenantcy();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
