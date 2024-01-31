using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VirtuagymDevTask.Data;
using VirtuagymDevTask.Services;

namespace VirtuagymDevTask
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
            services.AddControllers().AddNewtonsoftJson(options =>
            {   // for jsonignore globally
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                // to show enums as names globally
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GymCardDevTask", Version = "v1" });
            });
            services.AddDbContext<AppDbContext>(options =>
            {   // databse connection string with the database provider             
                options.UseNpgsql(
                Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddAutoMapper(typeof(IGymRepository).Assembly);
            services.AddScoped<IGymRepository, GymRepository>();
            services.AddScoped<ICheckInService, CheckInService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VirtuagymDevTask v1"));
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
