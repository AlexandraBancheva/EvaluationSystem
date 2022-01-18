using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentValidation.AspNetCore;
using EvaluationSystem.Persistence.Migrations;
using EvaluationSystem.Application.Middlewares;
using EvaluationSystem.Persistence.Configurations;
using EvaluationSystem.Application.ConfigurationServices;
using EvaluationSystem.Application.Profiles.ModuleProfile;
using EvaluationSystem.Application.Profiles.AnswerProfile;
using EvaluationSystem.Application.Profiles.QuestionProfile;
using EvaluationSystem.Application.Validations.QuestionValidations;
using EvaluationSystem.Application.Profiles.ModuleQuestionProfile;
using EvaluationSystem.Application.Profiles.FormProfile;
using EvaluationSystem.Application.Profiles.FormModuleProfile;
using EvaluationSystem.Persistence;
using EvaluationSystem.Application.Profiles.AttestationModuleProfile;
using EvaluationSystem.Application.Profiles.AttestationFormProfile;
using EvaluationSystem.Application.Profiles.AttestationAnswerProfile;
using EvaluationSystem.Application.Validations.ModuleValidations;

namespace EvaluationSystem.API
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
            services.AddConfigurationRepositories();
            services.AddConfigurationServices();

            services.AddControllers().AddFluentValidation(fv =>
                        fv.RegisterValidatorsFromAssemblyContaining<CreateQuestionValidation>());

            services.AddControllers().AddJsonOptions(options =>
                       options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddControllers();
            // Memory cache
            //services.AddMemoryCache();

            services.AddAutoMapper(typeof(QuestionProfile), 
                typeof(AnswerProfile), 
                typeof(ModuleProfile), 
                typeof(ModuleQuestionProfile), 
                typeof(FormProfile), 
                typeof(FormModuleProfile),
                typeof(AttestationModuleProfile),
                typeof(AttestationFormProfile),
                typeof(AttestationAnswerProfile));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EvaluationSystem.API", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "Using the Authorization header with the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                     { securitySchema, new[] { "Bearer" } }
                });
            });


            UseMigrator.UseMigrations(services);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = Configuration["Auth2:Domain"];
                options.Audience = Configuration["Auth2:Audience"];
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("3ec073da-256d-4a76-a135-846325d5f464"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "EvaluationSystem.API v1"));

            CreateDatabase.EnsureDatabase(Configuration);

            app.UpdateDatabase();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandleMiddleware>();
            app.UseMiddleware<UserTokenMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
