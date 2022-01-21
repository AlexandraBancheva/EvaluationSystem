using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Repositories
{
    public interface IUserAnswerRepository : IRepository<UserAnswer>
    {
        void ChangeStatusToDone(int attestationId, int idUserParticipant);

        void DeleteUserAnswerByAttestationId(int attestationId);

        void AddAnswerLikeATextField(int idAttestation, int idUser, int idAttestationModule, int idAttestationQuestion, string textAnswer);

        UserAnswer GetUserAnswerByAttestationId(int attestationId);

        ICollection<UserAnswer> GetAllAnswersByUser(int attestationId, int userId);

    }
}