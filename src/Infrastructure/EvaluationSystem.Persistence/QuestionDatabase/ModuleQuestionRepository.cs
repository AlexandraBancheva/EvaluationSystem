﻿using System.Linq;
using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Application.Models.ModuleQuestions;

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

        public ICollection<ModuleQuestionGettingAllQuestionIds> GetAllQuestionIdsByModuleId(int moduleId)
        {
            var query = @"SELECT IdQuestion FROM ModuleQuestion
                            WHERE IdModule = @IdModule";

            var results = _connection.Query<ModuleQuestionGettingAllQuestionIds>(query, new { IdModule = moduleId });

            return (ICollection<ModuleQuestionGettingAllQuestionIds>)results;
        }

        public ICollection<ModuleTemplateDto> GetAllQuestionsByModuleId(int moduleId)
        {
            var query = @"SELECT * FROM ModuleTemplate AS mt
                            LEFT JOIN ModuleQuestion AS mq ON mq.IdModule = mt.Id
                            LEFT JOIN QuestionTemplate AS qt ON mq.IdQuestion = qt.Id
                            WHERE mt.Id = @ModuleId";

            var queryParameter = new { ModuleId = moduleId };

            var moduleDictionary = new Dictionary<int, ModuleTemplateDto>();
            var moduleWishQuestions = _connection.Query<ModuleTemplateDto, QuestionTemplateDto, ModuleTemplateDto>(query, (module, question) =>
            {
                if (!moduleDictionary.TryGetValue(module.Id, out var currentModule))
                {
                    currentModule = module;
                    moduleDictionary.Add(currentModule.Id, currentModule);
                }

                currentModule.Questions.Add(question);
                return currentModule;
            }, queryParameter, _transaction,
               splitOn: "Id")
            .Distinct()
            .ToList();

            return moduleWishQuestions;
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

       // public ICollection<ModuleTemplateDto> GetModuleWithAllQuestionsAnswers(int moduleId)
       // {
       //     var query = @"SELECT * FROM ModuleTemplate AS mt
       //                     JOIN ModuleQuestion AS mq ON mt.Id = mq.IdModule
       //                     JOIN QuestionTemplate AS qt ON mq.IdQuestion = qt.Id
							//LEFT JOIN AnswerTemplate AS [at] ON qt.Id = [at].IdQuestion
							//WHERE mt.Id = @IdModule";

       //     var moduleDict = new Dictionary<int, ModuleTemplateDto>();
       //     var questionDict = new Dictionary<int, QuestionTemplateDto>();
       //     var results = _connection.Query<ModuleTemplateDto, QuestionTemplateDto, AnswerTemplate, ModuleTemplateDto>(query, (module, question, answer) =>
       //     {
       //         if (!moduleDict.TryGetValue(module.Id, out var currentModule))
       //         {
       //             currentModule = module;
       //             moduleDict.Add(currentModule.Id, currentModule);
       //         }

       //         if (!questionDict.TryGetValue(question.Id, out var currentQuestion))
       //         {
       //             currentQuestion = question;
       //             questionDict.Add(currentQuestion.Id, currentQuestion);
       //         }

       //         currentQuestion.Answers.Add(answer);
       //         currentModule.Questions.Add(question);
       //         return currentModule;
       //     }, new { IdModule = moduleId }, _transaction, splitOn: "Id").Distinct().ToList();

       //     return results;
       // }
    }
}
