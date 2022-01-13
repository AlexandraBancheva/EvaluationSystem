using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Persistence.EvaluationSystemDatabase;
using EvaluationSystem.Persistence.QuestionDatabase;
using Microsoft.Extensions.DependencyInjection;

namespace EvaluationSystem.Persistence.Configurations
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection AddConfigurationRepositories(this IServiceCollection services)
        {
           // services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IUnitOfWork, UnitOfWork>(); // => PROBLEM!!!
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<ICustomQuestionsRepository, CustomQuestionsRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IModuleQuestionRepository, ModuleQuestionRepository>();
            services.AddScoped<IFormRepository, FormRepository>();
            services.AddScoped<IFormModuleRepository, FormModuleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IAttestationQuestionRepository, AttestationQuestionRepository>();
            services.AddScoped<IAttestationAnswerRepository, AttestationAnswerRepository>();
            services.AddScoped<IAttestationModuleRepository, AttestationModuleRepository>();
            services.AddScoped<IAttestationModuleQuestionRepository, AttestationModuleQuestionRepository>();
            services.AddScoped<IAttestationFormRepository, AttestationFormRepository>();
            services.AddScoped<IAttestationFormModuleRepository, AttestationFormModuleRepository>();
            services.AddScoped<IAttestationRepository, AttestationRepository>();
            services.AddScoped<IAttestationParticipantRepository, AttestationParticipantRepository>();

            services.AddScoped<IUserAnswerRepository, UserAnswerRepository>();

            return services;
        }
    }
}
