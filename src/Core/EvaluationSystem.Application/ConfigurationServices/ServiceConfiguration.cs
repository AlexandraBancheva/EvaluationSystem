using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EvaluationSystem.Application.ConfigurationServices
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IQuestionTemplatesServices, QuestionTemplatesServices>();
            services.AddScoped<ICustomQuestionsServices, CustomQuestionsServices>();
            services.AddScoped<IAnswersServices, AnswersServices>();
            services.AddScoped<IModulesServices, ModulesServices>();
            services.AddScoped<IModuleQuestionsServices, ModuleQuestionsServices>();
            services.AddScoped<IFormsServices, FormsServices>();
            services.AddScoped<IFormModulesServices, FormModulesServices>();
        }
    }
}
