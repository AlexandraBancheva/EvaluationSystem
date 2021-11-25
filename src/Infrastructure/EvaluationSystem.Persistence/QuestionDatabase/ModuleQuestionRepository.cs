using System.Linq;
using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class ModuleQuestionRepository : BaseRepository<ModuleQuestion>, IModuleQuestionRepository
    {
        public ModuleQuestionRepository(IUnitOfWork unitOfWork)
           : base(unitOfWork)
        {
        }

        public void AddNewQuestionToModule(int moduleId, int questionId, int position)
        {
            var query = @"INSERT INTO ModuleQuestion
                            VALUES (@IdModule, @IdQuestion, @Position)";
            _connection.Execute(query, new { IdModule = moduleId, IdQuestion = questionId, Position = position}, _transaction);
        }

        public void DeleteQuestionFromModule(int moduleId, int questionId)
        {
            var query = @"DELETE ModuleQuestion
                            WHERE IdModule = @ModuleId AND IdQuestion = @QuestionId ";

            _connection.Execute(query, new { ModuleId = moduleId, QuestionId = questionId}, _transaction);
        }

        public ICollection<ModuleTemplateDto> GetModuleWithAllQuestions()
        {
            var query = @"SELECT * FROM ModuleTemplate AS mt
                            JOIN ModuleQuestion AS mq ON mt.Id = mq.IdModule
                            JOIN QuestionTemplate AS qt ON mq.IdQuestion = qt.Id";

            var moduleDictionary = new Dictionary<int, ModuleTemplateDto>();
            var modules = _connection.Query<ModuleTemplateDto, QuestionTemplateDto, ModuleTemplateDto>(query, (module, question) =>
            {
                if (!moduleDictionary.TryGetValue(module.Id, out var currentModule))
                {
                    currentModule = module;
                    moduleDictionary.Add(currentModule.Id, currentModule);
                }

                currentModule.Questions.Add(question);
                return currentModule;
            }, _transaction, splitOn: "Id")
                .Distinct()
                .ToList();

            return modules;
        }
    }
}
