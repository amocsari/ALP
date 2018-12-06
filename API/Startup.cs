using API.Service;
using API.Service.Interface;
using DAL.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DAL
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IAlpContext, ALPContext>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IBuildingService, BuildingService>();
            services.AddSingleton<IDepartmentService, DepartmentService>();
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddSingleton<IFloorService, FloorService>();
            services.AddSingleton<IItemStateService, ItemStateService>();
            services.AddSingleton<IItemNatureService, ItemNatureService>();
            services.AddSingleton<IItemService, ItemService>();
            services.AddSingleton<IItemTypeService, ItemTypeService>();
            services.AddSingleton<ILocationService, LocationService>();
            services.AddSingleton<IOperationService, OperationService>();
            services.AddSingleton<ISectionService, SectionService>();
            services.AddSingleton<IEncryptionService, EncryptionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            loggerFactory.AddFile(Configuration.GetSection("Logging"));

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
