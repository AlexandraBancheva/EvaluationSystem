using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Repositories
{
    public interface IUserAnswerRepository : IRepository<UserAnswer>
    {
        void ChangeStatusToDone(int attestationId, int idUserParticipant);

        void DeleteUserAnswerByAttestationId(int attestationId);

        void RemovedAnswerFromDb(int id);

        void UpdateTextFiledInUserAnswer(int idAttestation, int idUser, int idAttestationModule, int idAttestationQuestion, string textAnswer);

        UserAnswer GetUserAnswerByAttestationId(int attestationId, int questionId);

        UserAnswer GetUserAnswer(int attestationId, int questionId);

        UserAnswer GetUserAnswerTextFieldByAttestationId(int attestationId, int questionId);

        AttestationParticipant CheckParticipantStatusIsDone(int attestionId);

        ICollection<UserAnswer> GetAllAnswersByUser(int attestationId, int userId);

        ICollection<UserAnswer> GetAllUserAnswerWhenCheckBoxes(int attestationId, int questionId);
    }
}