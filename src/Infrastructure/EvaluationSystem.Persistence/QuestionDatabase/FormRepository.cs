using System.Linq;
using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class FormRepository : BaseRepository<FormTemplate>, IFormRepository
    {
        public FormRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public void DeleteForm(int formId)
        {
            var query = @"DELETE FROM FormModule
                            WHERE IdForm = @FormId
                            DELETE FROM FormTemplate
                            WHERE Id = @FormId";
            _connection.Execute(query, new { FormId = formId }, _transaction);
        }

        public IEnumerable<FormWithAllDto> AllForms()
        {
            // GroupBy???
            //var query = @"SELECT * FROM FormModule AS fm
            //                JOIN FormTemplate AS  ft ON ft.Id = fm.IdForm
            //                JOIN ModuleTemplate AS mt ON mt.Id = fm.IdModule
            //                JOIN ModuleQuestion AS mq ON mq.IdModule = mt.Id
            //                JOIN QuestionTemplate AS qt ON qt.Id = mq.IdQuestion
            //                LEFT JOIN AnswerTemplate AS [at] ON [at].IdQuestion = qt.Id";

            var query = @"SELECT *
                            FROM FormTemplate AS ft
                            LEFT JOIN FormModule AS fm ON fm.IdForm = ft.Id
                            LEFT JOIN ModuleTemplate AS mt ON fm.IdModule = mt.Id
                            LEFT JOIN ModuleQuestion AS mq ON mq.IdModule = mt.Id
                            LEFT JOIN QuestionTemplate AS qt ON mq.IdQuestion = qt.Id
                            LEFT JOIN AnswerTemplate AS [at] ON [at].IdQuestion = qt.Id";

            var formDictionary = new Dictionary<int, FormWithAllDto>();
            var moduleDictionary = new Dictionary<int, ModuleInFormDto>();
            var questionDictionary = new Dictionary<int, QuestionInModuleDto>();
            var forms = _connection.Query<FormWithAllDto, ModuleInFormDto, QuestionInModuleDto, AnswersInQuestionDto, FormWithAllDto>(query, (form, module, question, answer) =>
            {
                if (!formDictionary.TryGetValue(form.Id, out var currentForm))
                {
                    currentForm = form;
                    formDictionary.Add(currentForm.Id, currentForm);
                }

                if (!moduleDictionary.TryGetValue(module.Id, out var currentModule))
                {
                    currentModule = module;
                    moduleDictionary.Add(currentModule.Id, currentModule);
                }

                if (!questionDictionary.TryGetValue(question.Id, out var currentQuestion))
                {
                    currentQuestion = question;
                    questionDictionary.Add(currentQuestion.Id, currentQuestion);
                }

                currentQuestion.Answers.Add(answer);
                currentModule.Questions.Add(question);
                currentForm.Modules.Add(module);
                return currentForm;

            }, _transaction, splitOn: "Id, Id, Id, IdModuleQuestion, Id, Id")
                .Distinct()
                .ToList();

            return forms;
        }

        public IEnumerable<FormWithAllDto> GetAllWithFormId(int formId)
        {
            var query = @"SELECT ft.Id AS IdForm, ft.[Name], fm.Position, mt.Id AS IdModule, mt.[Name], mq.Position, qt.Id AS IdQuestion, qt.[Name], qt.DateOfCreation, qt.[Type], qt.IsReusable, [at].Id AS IdAnswer, [at].AnswerText, [at].IsDefault, [at].Position
                            FROM FormTemplate AS ft
                            INNER JOIN FormModule AS fm ON fm.IdForm = ft.Id
                            INNER JOIN ModuleTemplate AS mt ON fm.IdModule = mt.Id
                            INNER JOIN ModuleQuestion AS mq ON mq.IdModule = mt.Id
                            LEFT JOIN QuestionTemplate AS qt ON mq.IdQuestion = qt.Id
                            LEFT JOIN AnswerTemplate AS [at] ON [at].IdQuestion = qt.Id
                            WHERE IdForm = @IdForm";

            var formDictionary = new Dictionary<int, FormWithAllDto>();
            var moduleDictionary = new Dictionary<int, ModuleInFormDto>();
            var questionDictionary = new Dictionary<int, QuestionInModuleDto>();
            var results = _connection.Query<FormWithAllDto, ModuleInFormDto, QuestionInModuleDto, AnswersInQuestionDto, FormWithAllDto>(query, (form, module, question, answer) =>
            {
                if (!formDictionary.TryGetValue(form.Id, out var currentForm))
                {
                    currentForm = form;
                    formDictionary.Add(currentForm.Id, currentForm);
                }

                if (!moduleDictionary.TryGetValue(module.Id, out var currentModule))
                {
                    currentModule = module;
                    moduleDictionary.Add(currentModule.Id, currentModule);
                }


                if (!questionDictionary.TryGetValue(question.Id, out var currentQuestion))
                {
                    currentQuestion = question;
                    questionDictionary.Add(currentQuestion.Id, currentQuestion);
                }

                currentQuestion.Answers.Add(answer);
                currentModule.Questions.Add(question);
                currentForm.Modules.Add(module);
                return currentForm;

            }, new { IdForm = formId }, _transaction, splitOn: "IdForm, IdModule, IdQuestion, IdAnswer")
                .Distinct()
                .ToList();

            return results;
        }
    }
}