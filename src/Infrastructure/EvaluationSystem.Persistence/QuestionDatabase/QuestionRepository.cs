using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class QuestionRepository : IQuestionRepository
    {
        private FakeDatabase _fakeDatabase;

        public QuestionRepository(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public IEnumerable<Question> AllQuestions()
        {
            return _fakeDatabase.Questions;
        }

        public void CreateNewQuestion(Question model)
        {
            _fakeDatabase.Questions.Add(model); 
        }

        public Question GetQuestionById(int questionId)
        {
            return  _fakeDatabase.Questions.FirstOrDefault(i => i.Id == questionId);
        }

        public Question UpdateCurrentQuestion(Question model)
        {
            _fakeDatabase.Questions.Add(model);
            return model;
        }
    }
}
