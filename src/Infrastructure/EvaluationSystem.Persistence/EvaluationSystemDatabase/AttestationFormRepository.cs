using System.Linq;
using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Persistence.QuestionDatabase;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Persistence.EvaluationSystemDatabase
{
    public class AttestationFormRepository : BaseRepository<AttestationForm>, IAttestationFormRepository
    {
        public AttestationFormRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public void DeleteAttestationForm(int formId)
        {
            var query = @"DELETE FROM AttestationFormModule
                WHERE IdAttestationForm = @FormId
                DELETE FROM AttestationForm
                WHERE Id = @FormId";

            var queryParameter = new { FormId = formId };
            Connection.Execute(query, queryParameter, transaction: Transaction);
        }

        public ICollection<FormWithAllDto> GetAllByFormId(int formId)
        {
            var query = @"SELECT ft.Id AS Id, ft.[Name], mt.Id AS IdModule, mt.[Name], fm.Position, qt.Id AS IdQuestion, qt.[Name], qt.DateOfCreation, qt.[Type], qt.IsReusable, mq.Position, [at].Id AS IdAnswer, [at].AnswerText, [at].IsDefault, [at].Position
                            FROM AttestationForm AS ft
                            JOIN AttestationFormModule AS fm ON fm.IdAttestationForm = ft.Id
                            JOIN AttestationModule AS mt ON fm.IdAttestationModule = mt.Id
                            JOIN AttestationModuleQuestion AS mq ON mq.IdAttestationModule = mt.Id
                            JOIN AttestationQuestion AS qt ON mq.IdAttestationQuestion = qt.Id
                            LEFT JOIN AttestationAnswer AS [at] ON [at].IdQuestion = qt.Id
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
            var query = @"SELECT [Name] FROM AttestationForm";

            var names = Connection.Query<CheckFormNameDto>(query, transaction: Transaction);

            return (ICollection<CheckFormNameDto>)names;
        }
    }
}