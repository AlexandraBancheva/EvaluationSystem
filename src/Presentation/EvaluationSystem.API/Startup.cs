using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;
using EvaluationSystem.Persistence.Migrations;
using EvaluationSystem.Application.Middlewares;
using EvaluationSystem.Persistence.Configurations;
using EvaluationSystem.Application.ConfigurationServices;
using EvaluationSystem.Application.Profiles.AnswerProfile;
using EvaluationSystem.Application.Profiles.QuestionProfile;
using EvaluationSystem.Application.Validations.AnswerValidations;
using EvaluationSystem.Application.Validations.QuestionValidations;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
            services.AddControllers().AddFluentValidation(fv =>
                        fv.RegisterValidatorsFromAssemblyContaining<CreateQuestionValidation>());
            services.AddControllers().AddFluentValidation(fv => 
                        fv.RegisterValidatorsFromAssemblyContaining<UpdateQuestionValidation>());
            services.AddControllers().AddFluentValidation(fv =>
                        fv.RegisterValidatorsFromAssemblyContaining<CreateAnswerValidaton>());
            services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UpdateAnswerValidation>());

            //?
            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.InvalidModelStateResponseFactory = actionContext =>
            //    {
            //        return new BadRequestObjectResult(new
            //        {
            //            Code = 400,
            //            ErrorMessage = actionContext.ModelState.Values.SelectMany(x => x.Errors)
            //        .Select(e => e.ErrorMessage)
            //        });
            //    };
            //});

            // Memory cache
           //services.AddMemoryCache();

            services.AddAutoMapper(typeof(QuestionProfile), typeof(AnswerProfile));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EvaluationSystem.API", Version = "v1" });
            });

            RepositoryConfiguration.ConfigureServices(services);
            ServiceConfiguration.ConfigureServices(services);

            UseMigrator.UseMigrations(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EvaluationSystem.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UpdateDatabase();

            app.UseMiddleware<ErrorHandleMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
