using Dapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class ModuleRepository : BaseRepository<ModuleTemplate>, IModuleRepository
    {
        public ModuleRepository(IUnitOfWork unitOfWork)
           : base(unitOfWork)
        {
        }

        public void DeleteModule(int moduleId)
        {
            var query = @"DELETE ModuleQuestion
                            WHERE IdModule = @ModuleId
                            DELETE ModuleTemplate
                            WHERE Id = @ModuleId";
            _connection.Execute(query, new { ModuleId = moduleId }, _transaction);
        }

        public ModuleTemplate GetModuleById(int formId, int moduleId)
        {
            var query = @"SELECT * FROM FormModule AS fm
                            JOIN ModuleTemplate AS mt ON mt.Id = fm.IdModule
                            WHERE IdForm = @IdForm and IdModule = @IdModule";

            var result = _connection.QueryFirstOrDefault<ModuleTemplate>(query, new { IdForm = formId, IdModule = moduleId});

            return result;
        }

        public void UpdateModule(int formId, int moduleId, ModuleTemplate module)
        {
            var query = @"UPDATE ModuleTemplate
                            SET [Name] = @Name
                            FROM ModuleTemplate AS mt
                            JOIN FormModule AS fm ON mt.Id = fm.IdModule
                            JOIN FormTemplate AS ft ON ft.Id = fm.IdForm
                            WHERE IdForm = @IdForm and IdModule = IdModule";

            _connection.Execute(query, new { Name = module.Name, IdForm = formId, IdModule = moduleId});
        }
    }
}