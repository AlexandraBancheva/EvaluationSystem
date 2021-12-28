using Dapper;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Persistence.QuestionDatabase;
using System.Collections.Generic;
using System.Linq;

namespace EvaluationSystem.Persistence.EvaluationSystemDatabase
{
    public class AttestationQuestionRepository : BaseRepository<AttestationQuestion>, IAttestationQuestionRepository
    {
        public AttestationQuestionRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public void DeleteQuestion(int questionId)
        {
            var query = @"DELETE FROM AttestationAnswer
                        WHERE IdQuestion = @IdQuestion
                        DELETE FROM AttestationModuleQuestion
                        WHERE IdAttestationQuestion = @IdQuestion
                        DELETE FROM AttestationQuestion
                        WHERE Id = @IdQuestion";

            _connection.Execute(query, new { IdQuestion = questionId}, _transaction);
        }

        public ICollection<AttestationQuestion> GetAllById(int questionId)
        {
            var query = @"SELECT * FROM AttestationQuestion AS aq
                        LEFT JOIN AttestationAnswer AS aa ON aa.IdQuestion = aq.Id";

            var queryParameter = new { QuestionId = questionId };

            var questionDictionary = new Dictionary<int, AttestationQuestion>();
            var questions = _connection.Query<AttestationQuestion, AttestationAnswer, AttestationQuestion>(query, (question, answer) =>
            {
                if (!questionDictionary.TryGetValue(question.Id, out var currentQuestion))
                {
                    currentQuestion = question;
                    questionDictionary.Add(currentQuestion.Id, currentQuestion);
                }

                currentQuestion.AttestationAnswers.Add(answer);
                return currentQuestion;
            }, queryParameter, _transaction,
               splitOn: "Id")
            .Distinct()
            .ToList();

            return questions;
        }

        public ICollection<AttestationQuestion> GetAllQuestionsWithAnswers()
        {
            var query = @"SELECT *
                            FROM AttestationQuestion AS aq
                            LEFT JOIN AttestationAnswer AS aa ON aq.Id = aa.IdQuestion";
            var questionDictionary = new Dictionary<int, AttestationQuestion>();
            var questions = _connection.Query<AttestationQuestion, AttestationAnswer, AttestationQuestion>(query, (question, answer) =>
            {
                if (!questionDictionary.TryGetValue(question.Id, out var currentQuestion))
                {
                    currentQuestion = question;
                    questionDictionary.Add(currentQuestion.Id, currentQuestion);
                }

                currentQuestion.AttestationAnswers.Add(answer);
                return currentQuestion;
            }, _transaction,
               splitOn: "Id")
            .Distinct()
            .ToList();

            return questions;
        }
    }
}