using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Application.ConfigurationServices
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IQuestionsServices, QuestionsServices>();
            services.AddScoped<IAnswersServices, AnswersServices>();
            services.AddScoped<IModulesServices, ModulesServices>();
        }
    }
}
