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

        //public void InsertAnswer(int questionId, AddNewAnswerDto model)
        //{
        //    var query = @"INSERT INTO AnswerTemplate
	       //                 VALUES (@Position, @AnswerText, @IdQuestion)";

        //    _connection.Execute(query, new { model.Position, model.AnswerText, IdQuestion = questionId }, _transaction);
        //}
    }
}