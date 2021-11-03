using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Profiles.AnswerProfile;
using EvaluationSystem.Application.Profiles.QuestionProfile;
using EvaluationSystem.Application.Services;
using EvaluationSystem.Application.Validations.AnswerValidations;
using EvaluationSystem.Application.Validations.QuestionValidations;
using EvaluationSystem.Persistence.QuestionDatabase;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;


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
                        fv.RegisterValidatorsFromAssemblyContaining<CreateAnswerValidaton>());

            services.AddAutoMapper(typeof(QuestionProfile), typeof(AnswerProfile));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EvaluationSystem.API", Version = "v1" });
            });

            services.AddSingleton<FakeDatabase>();

            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();

            services.AddScoped<IQuestionsServices, QuestionsServices>();
            services.AddScoped<IAnswersServices, AnswersServices>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
