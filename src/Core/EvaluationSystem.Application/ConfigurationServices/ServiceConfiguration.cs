using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Users;
using EvaluationSystem.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EvaluationSystem.Application.ConfigurationServices
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddConfigurationServices(this IServiceCollection services)
        {
            services.AddScoped<IQuestionTemplatesServices, QuestionTemplatesServices>();
            services.AddScoped<ICustomQuestionsServices, CustomQuestionsServices>();
            services.AddScoped<IAnswersServices, AnswersServices>();
            services.AddScoped<IModulesServices, ModulesServices>();
            services.AddScoped<IModuleQuestionsServices, ModuleQuestionsServices>();
            services.AddScoped<IFormsServices, FormsServices>();
            services.AddScoped<IFormModulesServices, FormModulesServices>();
            services.AddScoped<IUsersServices, UsersServices>();
            services.AddScoped<IUser, CurrentUser>();
            services.AddScoped<IAttestationQuestionsServices, AttestationQuestionsServices>();
            services.AddScoped<IAttestationAnswersServices, AttestationAnswersServices>();
            services.AddScoped<IAttestationModulesServices, AttestationModulesServices>();
            services.AddScoped<IAttestationFormsServices, AttestationFormsServices>();
            services.AddScoped<IAttestationFormModulesServices, AttestationFormModulesServices>();
            services.AddScoped<IAttestationsServices, AttestationsServices>();
            services.AddScoped<IUserAnswersServices, UserAnswersServices>();

            return services;
        }
    }
}
