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

            var queryParameter = new { FormId = formId };
            Connection.Execute(query, queryParameter, transaction: Transaction);
        }

        public ICollection<FormWithAllDto> GetAllForms()
        {
            var query = @"SELECT ft.Id AS Id, ft.[Name], mt.Id AS IdModule, mt.[Name], fm.Position, qt.Id AS IdQuestion, qt.[Name], qt.DateOfCreation, qt.[Type], qt.IsReusable, mq.Position, [at].Id AS IdAnswer, [at].AnswerText, [at].IsDefault, [at].Position
                            FROM FormTemplate AS ft
                            LEFT JOIN FormModule AS fm ON fm.IdForm = ft.Id
                            LEFT JOIN ModuleTemplate AS mt ON fm.IdModule = mt.Id
                            LEFT JOIN ModuleQuestion AS mq ON mq.IdModule = mt.Id
                            LEFT JOIN QuestionTemplate AS qt ON mq.IdQuestion = qt.Id
                            LEFT JOIN AnswerTemplate AS [at] ON [at].IdQuestion = qt.Id";

            var formDictionary = new Dictionary<int, FormWithAllDto>();
            var moduleDictionary = new Dictionary<int, ModuleInFormDto>();
            var questionDictionary = new Dictionary<int, QuestionInModuleDto>();
            var forms = Connection.Query<FormWithAllDto, ModuleInFormDto, QuestionInModuleDto, AnswersInQuestionDto, FormWithAllDto>(query, (form, module, question, answer) =>
            {
                if (!formDictionary.TryGetValue(form.Id, out var currentForm))
                {
                    currentForm = form;
                    formDictionary.Add(currentForm.Id, currentForm);
                }

                if (module != null)
                {
                    if (!moduleDictionary.TryGetValue(module.IdModule, out var currentModule))
                    {
                        currentModule = module;
                        moduleDictionary.Add(currentModule.IdModule, currentModule);
                    }

                    if (question != null)
                    {
                        if (!questionDictionary.TryGetValue(question.IdQuestion, out var currentQuestion))
                        {
                            currentQuestion = question;
                            questionDictionary.Add(currentQuestion.IdQuestion, currentQuestion);
                        }

                        currentQuestion.Answers.Add(answer);
                    }
                   
                    currentModule.Questions.Add(question);
                }
                
                currentForm.Modules.Add(module);
                return currentForm;

            }, transaction: Transaction, splitOn: "Id, IdModule, IdQuestion, IdAnswer")
                .Distinct()
                .ToList();

            return forms;
        }

        public ICollection<FormWithAllDto> GetAllByFormId(int formId)
        {
            var query = @"SELECT ft.Id AS Id, ft.[Name], mt.Id AS IdModule, mt.[Name], fm.Position, qt.Id AS IdQuestion, qt.[Name], qt.DateOfCreation, qt.[Type], qt.IsReusable, mq.Position, [at].Id AS IdAnswer, [at].AnswerText, [at].IsDefault, [at].Position
                            FROM FormTemplate AS ft
                            LEFT JOIN FormModule AS fm ON fm.IdForm = ft.Id
                            LEFT JOIN ModuleTemplate AS mt ON fm.IdModule = mt.Id
                            LEFT JOIN ModuleQuestion AS mq ON mq.IdModule = mt.Id
                            LEFT JOIN QuestionTemplate AS qt ON mq.IdQuestion = qt.Id
                            LEFT JOIN AnswerTemplate AS [at] ON [at].IdQuestion = qt.Id
                            WHERE ft.Id = @IdForm";

            var queryParameter = new { IdForm = formId };
            var formDictionary = new Dictionary<int, FormWithAllDto>();
            var moduleDictionary = new Dictionary<int, ModuleInFormDto>();
            var questionDictionary = new Dictionary<int, QuestionInModuleDto>();
            var results = Connection.Query<FormWithAllDto, ModuleInFormDto, QuestionInModuleDto, AnswersInQuestionDto, FormWithAllDto>(query, (form, module, question, answer) =>
            {
                if (!formDictionary.TryGetValue(form.Id, out var currentForm))
                {
                    currentForm = form;
                    formDictionary.Add(currentForm.Id, currentForm);
                }

                if (module != null)
                {
                    if (!moduleDictionary.TryGetValue(module.IdModule, out var currentModule))
                    {
                        currentModule = module;
                        moduleDictionary.Add(currentModule.IdModule, currentModule);
                    }

                    if (question != null)
                    {
                        if (!questionDictionary.TryGetValue(question.IdQuestion, out var currentQuestion))
                        {
                            currentQuestion = question;
                            questionDictionary.Add(currentQuestion.IdQuestion, currentQuestion);
                        }

                        currentQuestion.Answers.Add(answer);
                    }

                    currentModule.Questions.Add(question);
                }


                currentForm.Modules.Add(module);
                return currentForm;

            }, queryParameter, transaction: Transaction, splitOn: "Id, IdModule, IdQuestion, IdAnswer")
                .Distinct()
                .ToList();

            return results;
        }

        public ICollection<CheckFormNameDto> GetAllFormNames()
        {
            var query = @"SELECT [Name] FROM FormTemplate";

            var names = Connection.Query<CheckFormNameDto>(query, transaction: Transaction);

            return (ICollection<CheckFormNameDto>)names;
        }
    }
}