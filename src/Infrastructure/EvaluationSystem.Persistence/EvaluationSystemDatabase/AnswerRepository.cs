﻿using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Repositories;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class AnswerRepository : BaseRepository<AnswerTemplate>, IAnswerRepository
    {
        public AnswerRepository(IUnitOfWork unitOfWork)
           : base(unitOfWork)
        {
        }

        public ICollection<AnswerTemplate> GetAllByQuestionId(int questionId)
        {
            var query = @"SELECT * FROM AnswerTemplate
                        WHERE IdQuestion = @QuestionId";

            var answers = _connection.Query<AnswerTemplate>(query, new { QuestionId = questionId}, _transaction);

            return (ICollection<AnswerTemplate>)answers;
        }
    }
}