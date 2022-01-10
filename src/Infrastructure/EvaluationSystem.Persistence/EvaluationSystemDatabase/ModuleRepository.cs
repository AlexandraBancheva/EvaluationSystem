using Dapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;

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
            var query = @"DELETE FormModule
                            WHERE IdModule = @ModuleId
                            DELETE ModuleQuestion
                            WHERE IdModule = @ModuleId
                            DELETE ModuleTemplate
                            WHERE Id = @ModuleId";
            Connection.Execute(query, new { ModuleId = moduleId }, Transaction);
        }

        public ModuleTemplateDto GetModuleById(int formId, int moduleId)
        {
            var query = @"SELECT mt.Id, mt.[Name], fm.Position AS ModulePosition
                            FROM FormModule AS fm
                            LEFT JOIN ModuleTemplate AS mt ON mt.Id = fm.IdModule
                            WHERE IdForm = @IdForm and IdModule = @IdModule";

            var result = Connection.QueryFirstOrDefault<ModuleTemplateDto>(query, new { IdForm = formId, IdModule = moduleId}, Transaction);

            return result;
        }

        public void UpdateModule(int formId, int moduleId, ModuleTemplate module)
        {
            var query = @"UPDATE ModuleTemplate
                            SET [Name] = @Name
                            FROM ModuleTemplate AS mt
                            JOIN FormModule AS fm ON mt.Id = fm.IdModule
                            JOIN FormTemplate AS ft ON ft.Id = fm.IdForm
                            WHERE IdForm = @IdForm and IdModule = @IdModule";

            Connection.Execute(query, new { module.Name, IdForm = formId, IdModule = moduleId}, Transaction);
        }
    }
}