using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly FakeDatabase fakeDatabase;

        public AnswerRepository(FakeDatabase fakeDatabase)
        {
            this.fakeDatabase = fakeDatabase;
        }

        public void AddNewAnswer(Answer model)
        {
            fakeDatabase.Answers.Add(model);
        }

        public void DeleteAnAnswer(int questionId, int answerId)
        {
            var currrentEntity = fakeDatabase.Answers.FirstOrDefault(a => a.Id == answerId);
            fakeDatabase.Answers.Remove(currrentEntity);
        }

        public IEnumerable<Answer> GetAllAnswersByQuestionId(int questionId)
        {
            return fakeDatabase.Answers.Where(a => a.QuestionId == questionId);
        }
    }
}
