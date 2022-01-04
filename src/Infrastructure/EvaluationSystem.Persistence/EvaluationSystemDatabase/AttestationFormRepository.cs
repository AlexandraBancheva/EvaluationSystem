using System.Linq;
using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Persistence.EvaluationSystemDatabase
{
    public class AttestationFormRepository : QuestionDatabase.BaseRepository<AttestationForm>, IAttestationFormRepository
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

            _connection.Execute(query, new {FormId = formId }, _transaction);
        }

        public ICollection<FormWithAllDto> GetAllByFormId(int formId)
        {
            var query = @"SELECT ft.Id AS Id, ft.[Name], mt.Id AS IdModule, mt.[Name], fm.Position, qt.Id AS IdQuestion, qt.[Name], qt.DateOfCreation, qt.[Type], qt.IsReusable, mq.Position, [at].Id AS IdAnswer, [at].AnswerText, [at].IsDefault, [at].Position
                            FROM AttestationForm AS ft
                            LEFT JOIN AttestationFormModule AS fm ON fm.IdAttestationForm = ft.Id
                            LEFT JOIN AttestationModule AS mt ON fm.IdAttestationModule = mt.Id
                            LEFT JOIN AttestationModuleQuestion AS mq ON mq.IdAttestationModule = mt.Id
                            LEFT JOIN AttestationQuestion AS qt ON mq.IdAttestationQuestion = qt.Id
                            LEFT JOIN AttestationAnswer AS [at] ON [at].IdQuestion = qt.Id
                            WHERE ft.Id = @IdForm";


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

            }, new { IdForm = formId }, _transaction, splitOn: "Id, IdModule, IdQuestion, IdAnswer")
                .Distinct()
                .ToList();

            return results;
        }

        public ICollection<CheckFormNameDto> GetAllFormNames()
        {
            var query = @"SELECT [Name] FROM AttestationForm";

            var names = _connection.Query<CheckFormNameDto>(query, _transaction);

            return (ICollection<CheckFormNameDto>)names;
        }
    }
}
