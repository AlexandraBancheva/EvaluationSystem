using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Persistence.QuestionDatabase;
using Microsoft.Extensions.DependencyInjection;

namespace EvaluationSystem.Persistence.Configurations
{
    public static class RepositoryConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IModuleQuestionRepository, ModuleQuestionRepository>();
            services.AddScoped<IFormRepository, FormRepository>();
            services.AddScoped<IFormModuleRepository, FormModuleRepository>();
        }
    }
}
