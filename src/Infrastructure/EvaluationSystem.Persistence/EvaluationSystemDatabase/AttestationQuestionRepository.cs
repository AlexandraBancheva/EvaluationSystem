using System.Linq;
using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Repositories;

namespace EvaluationSystem.Persistence.EvaluationSystemDatabase
{
    public class AttestationQuestionRepository : QuestionDatabase.BaseRepository<AttestationQuestion>, IAttestationQuestionRepository
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

            var queryParameter = new { IdQuestion = questionId };
            Connection.Execute(query, queryParameter, transaction: Transaction);
        }

        public ICollection<AttestationQuestion> GetAllById(int questionId)
        {
            var query = @"SELECT * FROM AttestationQuestion AS aq
                        LEFT JOIN AttestationAnswer AS aa ON aa.IdQuestion = aq.Id";

            var queryParameter = new { QuestionId = questionId };
            var questionDictionary = new Dictionary<int, AttestationQuestion>();
            var questions = Connection.Query<AttestationQuestion, AttestationAnswer, AttestationQuestion>(query, (question, answer) =>
            {
                if (!questionDictionary.TryGetValue(question.Id, out var currentQuestion))
                {
                    currentQuestion = question;
                    questionDictionary.Add(currentQuestion.Id, currentQuestion);
                }
                currentQuestion.AttestationAnswers.Add(answer);
                return currentQuestion;
            }, queryParameter, transaction: Transaction,
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
            var questions = Connection.Query<AttestationQuestion, AttestationAnswer, AttestationQuestion>(query, (question, answer) =>
            {
                if (!questionDictionary.TryGetValue(question.Id, out var currentQuestion))
                {
                    currentQuestion = question;
                    questionDictionary.Add(currentQuestion.Id, currentQuestion);
                }
                currentQuestion.AttestationAnswers.Add(answer);
                return currentQuestion;
            }, transaction: Transaction,
               splitOn: "Id")
            .Distinct()
            .ToList();

            return questions;
        }
    }
}