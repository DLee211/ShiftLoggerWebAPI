using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ShiftLoggerWebAPI;

using Microsoft.OpenApi.Models;
using ShiftLoggerWebAPI.Services;

CreateHostBuilder(args).Build().Run();
Console.ReadLine();

IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.ConfigureServices((context, services) =>
            {
                // Add DbContext
                services.AddDbContext<ShiftDbContext>(options =>
                    options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

                // Add services
                services.AddScoped<ShiftService>();

                // Add Swagger
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShiftAPI", Version = "v1" });
                    var xmlPath =
                        "bin/Debug/ShiftLoggerWebAPI.xml";
                    c.IncludeXmlComments(xmlPath);
                });

                services.AddControllers();
            });

            webBuilder.Configure((context, app) =>
            {
                if (context.HostingEnvironment.IsDevelopment())
                {
                    // Enable Swagger only in the development environment
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShiftAPI");
                    });
                }

                app.UseHttpsRedirection();
                app.UseRouting();
                app.UseAuthorization();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            });
        });