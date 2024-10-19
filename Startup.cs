using ACME_Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ACME_Api.MockDB;

namespace ACME_Api
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
            // Registrar MockDatabase como singleton
            services.AddSingleton<MockDatabase>();

            // Configuración de AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfile));

            // Agregar controladores
            services.AddControllers();
            
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<EnrollmentValidator>();

            // Configuración de Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "School Management API",
                    Version = "v1",
                    Description = "API para la gestión de estudiantes y cursos"
                });
            });

            // Configuración opcional de CORS si se requiere
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();  // Solo para producción
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "School Management API v1");
                c.RoutePrefix = string.Empty; // Hace que Swagger esté en la raíz
            });

            app.UseHttpsRedirection();

            // Omitir StaticFiles si no es necesario
            app.UseRouting();

            // Habilitar CORS si se configuró
            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
