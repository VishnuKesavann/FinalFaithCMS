using FinalCMS.LabRepository;
using FinalCMS.Doctor_Repository;
using FinalCMS.AdminRepository;
using FinalCMS.Models;
using FinalCMS.Repository;
using FinalCMS.Receptionist_Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FinalCMS
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
            services.AddControllers();

            services.AddScoped<IDAppointmentRepository , DAppointmentRepository>();
            services.AddScoped<IDPatientviewRepository, DPatientviewRepository>();
            services.AddScoped<IDPatienthistoryRepository , DPatienthistoryRepository>();
            services.AddScoped<IDdiagnosisRepositor , DdiagnosisRepository>();

            services.AddLogging();


            //connectionString for database , inject as dependency
            services.AddDbContext<FinalCMS_dbContext>(db => db.UseSqlServer(Configuration.GetConnectionString("connectionstring")));
            //add dependency injection of  Medicinerepository
            services.AddScoped<IMedicineRepository, MedicineRepository>();
            //add dependency injection for Laboratoryrepository
            services.AddScoped<ILaboratoryRepository, LaboratoryRepository>();
            //add dependency injection for staffrepository
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IUserLoginRepository, UserLoginRepository>();


            //add dependency injection of EmployeeRepository
            services.AddScoped<ILabreportRepository, LabreportRepository>();
            services.AddScoped<ILabTestList, LabTestList>();

            // Add repositories
            services.AddScoped<IPharmacistRepository, PharmacistRepository>();
            services.AddScoped<IPharPatientPrescriptionRepository, PharPatientPrescriptionRepository>();


            //json resolved
            services.AddControllers().AddNewtonsoftJson(Options =>
            {
                Options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            //enable cors
            services.AddCors();

            
            // Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Clinic Management System", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            //add service for patient repository
            services.AddScoped<IPatientRepository, PatientRepository>();
            //add service for appointment repository
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable CORS
            app.UseCors(options =>
            options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());



            app.UseCors(Options =>
                 Options.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 );



            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable Swagger UI only in the development environment
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clinic Management API Vi"));


            }

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
