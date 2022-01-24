using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Persistence.QuestionDatabase;

namespace EvaluationSystem.Persistence.EvaluationSystemDatabase
{
    public class UserAnswerRepository : BaseRepository<UserAnswer>, IUserAnswerRepository
    {
        public UserAnswerRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public void UpdateTextFiledInUserAnswer(int idAttestation, int idUser, int idAttestationModule, int idAttestationQuestion, string textAnswer)
        {
            var query = @"UPDATE [UserAnswer]
                        SET TextAnswer = @TextAnswer
                        WHERE IdAttestation = @IdAttestation AND IdUserParticipant = @IdUser AND  IdAttestationModule = @IdAttestationModule AND IdAttestationQuestion = @IdAttestationQuestion";

            Connection.Execute(query, new { TextAnswer = textAnswer,
                                            IdAttestation = idAttestation, 
                                            IdUser = idUser, 
                                            IdAttestationModule = idAttestationModule, 
                                            IdAttestationQuestion = idAttestationQuestion }, 
                             transaction: Transaction);
        }

        public void ChangeStatusToDone(int attestationId, int idUserParticipant)
        {
            var query = @"UPDATE AttestationParticipant
                        SET [Status] = 'Done'
                        WHERE IdAttestation = @AttestationId AND IdUserParticipant = @IdUserParticipant";

            Connection.Execute(query, new { AttestationId = attestationId, IdUserParticipant = idUserParticipant }, transaction: Transaction);
        }

        public void DeleteUserAnswerByAttestationId(int attestationId)
        {
            var query = @"DELETE [UserAnswer]
                        WHERE IdAttestation = @AttestationId";

            Connection.Execute(query, new { AttestationId = attestationId }, transaction: Transaction);
        }

        public ICollection<UserAnswer> GetAllAnswersByUser(int attestationId, int userId)
        {
            var query = @"SELECT * FROM [UserAnswer] AS ua
                        JOIN [AttestationParticipant] AS ap ON ua.IdAttestation = ap.IdAttestation
                        WHERE ua.IdAttestation = @IdAttestation AND ap.IdUserParticipant = @IdUserParticipant";

            var results = Connection.Query<UserAnswer>(query, new { IdAttestation = attestationId, IdUserParticipant = userId }, transaction: Transaction);

            return (ICollection<UserAnswer>)results;
        }

        public ICollection<UserAnswer> GetAllUserAnswerWhenCheckBoxes(int attestationId, int questionId)
        {
            var query = @"SELECT * FROM [UserAnswer]
                       WHERE IdAttestation = @AttestationId AND IdAttestationQuestion = @QuestionId";

            var results = Connection.Query<UserAnswer>(query, new { AttestationId = attestationId, QuestionId = questionId }, transaction: Transaction);

            return (ICollection<UserAnswer>)results;
        }

        public UserAnswer GetUserAnswer(int attestationId, int questionId)
        {
            var query = @"SELECT * FROM [UserAnswer]
                       WHERE IdAttestation = @AttestationId AND IdAttestationQuestion = @QuestionId";

            var result = Connection.QueryFirstOrDefault<UserAnswer>(query, new { AttestationId = attestationId, QuestionId = questionId }, transaction: Transaction);
            return result;
        }

        public UserAnswer GetUserAnswerByAttestationId(int attestationId, int questionId) // If hasn't any textAnswer
        {
            var query = @"SELECT [at].Id, [am].Id AS IdAttestationModule, aq.Id AS IdAttestationQuestion
                        FROM [Attestation] AS [at]
                        JOIN AttestationForm AS af ON [at].IdForm = af.Id
                        JOIN AttestationFormModule AS afm ON afm.IdAttestationForm = af.Id
                        JOIN AttestationModule AS am ON afm.IdAttestationModule = am.Id
                        JOIN AttestationModuleQuestion AS amq ON amq.IdAttestationModule = am.Id
                        JOIN AttestationQuestion AS aq ON amq.IdAttestationQuestion = aq.Id
                        LEFT JOIN AttestationAnswer AS aa ON aa.IdQuestion = amq.IdAttestationQuestion
                        WHERE [at].Id = @AttestationId AND aq.[Id] = @QuestionId";

            var res = Connection.QueryFirstOrDefault<UserAnswer>(query, new { AttestationId = attestationId, QuestionId = questionId }, transaction: Transaction);

            return res;
        }

        public UserAnswer GetUserAnswerTextFieldByAttestationId(int attestationId, int questionId) // If has some textAnswer
        {
            var query = @"SELECT [at].Id, [am].Id AS IdAttestationModule, aq.Id AS IdAttestationQuestion, ua.IdUserParticipant, ua.TextAnswer
                              FROM[Attestation] AS[at]
                              JOIN[UserAnswer] AS ua ON ua.IdAttestation = [at].Id
                              JOIN AttestationForm AS af ON[at].IdForm = af.Id
                              JOIN AttestationFormModule AS afm ON afm.IdAttestationForm = af.Id
                              JOIN AttestationModule AS am ON afm.IdAttestationModule = am.Id
                              JOIN AttestationModuleQuestion AS amq ON amq.IdAttestationModule = am.Id
                              JOIN AttestationQuestion AS aq ON amq.IdAttestationQuestion = aq.Id
                              LEFT JOIN AttestationAnswer AS aa ON aa.IdQuestion = amq.IdAttestationQuestion
                              WHERE[at].Id = @AttestationId AND aq.[Id] = @QuestionId";

            var res = Connection.QueryFirstOrDefault<UserAnswer>(query, new { AttestationId = attestationId, QuestionId =  questionId }, transaction: Transaction);

            return res;
        }

        public void RemovedAnswerFromDb(int id)
        {
            var query = @"DELETE [UserAnswer]
                        WHERE  Id = @Id";

            Connection.Execute(query, new { Id = id }, transaction: Transaction);
        }
    }
}