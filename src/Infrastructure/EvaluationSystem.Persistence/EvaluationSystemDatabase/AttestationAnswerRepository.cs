﻿using Dapper;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Persistence.QuestionDatabase;
using System.Collections.Generic;

namespace EvaluationSystem.Persistence.EvaluationSystemDatabase
{
    public class AttestationAnswerRepository : BaseRepository<AttestationAnswer>, IAttestationAnswerRepository
    {
        public AttestationAnswerRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public ICollection<AttestationAnswer> GetAllByQuestionId(int questionId)
        {
            var query = @"SELECT * FROM AttestationAnswer
                        WHERE IdQuestion = @QuestionId";

            var attestationAnswers = _connection.Query<AttestationAnswer>(query, new { QuestionId = questionId }, _transaction);

            return (ICollection<AttestationAnswer>)attestationAnswers;
        }
    }
}
