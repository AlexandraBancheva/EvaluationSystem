using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Persistence.QuestionDatabase;
using Microsoft.Extensions.DependencyInjection;

namespace EvaluationSystem.Persistence.Configurations
{
    public static class RepositoryConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
        }
    }
}
