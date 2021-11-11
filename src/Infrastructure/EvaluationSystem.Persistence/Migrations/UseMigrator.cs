using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EvaluationSystem.Persistence.Migrations
{
    public static class UseMigrator
    {
        public static  void UseMigrations(IServiceCollection services)
        {
            services.AddFluentMigratorCore()
                .ConfigureRunner(config =>
                {
                    config.AddSqlServer()
                    .WithGlobalConnectionString("DefaultConnection")
                    .ScanIn(Assembly.GetExecutingAssembly()).For.All();
                });
        }

        public static void UpdateDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
            migrator.MigrateUp();
        }
    }
}
