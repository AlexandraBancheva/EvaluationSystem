using System.Linq;
using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Models.ModuleQuestions;
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
            Connection.Execute(query, new { IdModule = moduleId, IdQuestion = questionId, Position = position}, transaction: Transaction);
        }

        public void DeleteQuestionFromModule(int moduleId, int questionId)
        {
            var query = @"DELETE FROM ModuleQuestion
                            WHERE IdModule = @ModuleId AND IdQuestion = @QuestionId ";

            Connection.Execute(query, new { ModuleId = moduleId, QuestionId = questionId}, transaction: Transaction);
        }

        public ICollection<ModuleQuestionGettingAllQuestionIds> GetAllQuestionIdsByModuleId(int moduleId)
        {
            var query = @"SELECT IdQuestion FROM ModuleQuestion
                            WHERE IdModule = @IdModule";

            var queryParameter = new { IdModule = moduleId };
            var results = Connection.Query<ModuleQuestionGettingAllQuestionIds>(query, queryParameter, transaction: Transaction);

            return (ICollection<ModuleQuestionGettingAllQuestionIds>)results;
        }

        public ICollection<ModuleTemplateDto> GetAllQuestionsByModuleId(int moduleId)
        {

            var query = @"SELECT mt.Id AS Id, mt.[Name], fm.Position AS ModulePosition, qt.Id AS Id, qt.[Name], qt.DateOfCreation, qt.[Type], qt.IsReusable, mq.Position AS QuestionPosition
                            FROM ModuleTemplate AS mt
                            JOIN FormModule AS fm ON fm.IdModule = mt.Id
                            LEFT JOIN ModuleQuestion AS mq ON mq.IdModule = mt.Id
                            LEFT JOIN QuestionTemplate AS qt ON mq.IdQuestion = qt.Id
                            WHERE mt.Id = @ModuleId";

            var queryParameter = new { ModuleId = moduleId };

            var moduleDictionary = new Dictionary<int, ModuleTemplateDto>();
            var moduleWishQuestions = Connection.Query<ModuleTemplateDto, QuestionTemplateDto, ModuleTemplateDto>(query, (module, question) =>
            {
                if (!moduleDictionary.TryGetValue(module.Id, out var currentModule))
                {
                    currentModule = module;
                    moduleDictionary.Add(currentModule.Id, currentModule);
                }
                currentModule.Questions.Add(question);
                return currentModule;
            }, queryParameter, transaction: Transaction,
               splitOn: "Id, Id")
            .Distinct()
            .ToList();

            return moduleWishQuestions;
        }

        public ICollection<ModuleQuestion> GetModuleWithAllQuestionIds(int moduleId)
        {
            var query = @"SELECT * FROM ModuleQuestion
                            WHERE IdModule = @ModuleId";

            var questionsIds = Connection.Query<ModuleQuestion>(query, transaction: Transaction);

            return (ICollection<ModuleQuestion>)questionsIds;
        }

        public ICollection<ModuleTemplateDto> GetModuleWithAllQuestions()
        {
            var query = @"SELECT * FROM ModuleTemplate AS mt
                            JOIN ModuleQuestion AS mq ON mt.Id = mq.IdModule
                            JOIN QuestionTemplate AS qt ON mq.IdQuestion = qt.Id";

            var moduleDictionary = new Dictionary<int, ModuleTemplateDto>();
            var modules = Connection.Query<ModuleTemplateDto, QuestionTemplateDto, ModuleTemplateDto>(query, (module, question) =>
            {
                if (!moduleDictionary.TryGetValue(module.Id, out var currentModule))
                {
                    currentModule = module;
                    moduleDictionary.Add(currentModule.Id, currentModule);
                }
                currentModule.Questions.Add(question);
                return currentModule;
            }, transaction: Transaction, splitOn: "Id")
                .Distinct()
                .ToList();

            return modules;
        }
    }
}
