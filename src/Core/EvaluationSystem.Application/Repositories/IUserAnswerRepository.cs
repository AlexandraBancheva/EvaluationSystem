using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Repositories
{
    public interface IUserAnswerRepository : IRepository<UserAnswer>
    {
        void ChangeStatusToDone(int attestationId, int idUserParticipant);

        void DeleteUserAnswerByAttestationId(int attestationId);

        void RemovedAnswerFromDb(int id); // 24.01.2022

        void UpdateTextFiledInUserAnswer(int idAttestation, int idUser, int idAttestationModule, int idAttestationQuestion, string textAnswer);

        UserAnswer GetUserAnswerByAttestationId(int attestationId, int questionId); // 24.01.2022

        UserAnswer GetUserAnswer(int attestationId, int questionId); // 24.01.2022

        UserAnswer GetUserAnswerTextFieldByAttestationId(int attestationId, int questionId); // 24.01.2022

        ICollection<UserAnswer> GetAllAnswersByUser(int attestationId, int userId);

        ICollection<UserAnswer> GetAllUserAnswerWhenCheckBoxes(int attestationId, int questionId); // 24.10.2022
    }
}