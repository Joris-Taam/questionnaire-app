using Questionnaire_Back_End.Core.DTOs;
using Questionnaire_Back_End.Data.DbContext;

namespace Questionnaire_Back_End.Core.Interfaces
{
    public interface IParticipantAnswerRepository
    {
        Task AddParticipantAnswerAsync(ParticipantAnswers participantAnswer);
        Task<IEnumerable<ParticipantAnswers>> GetParticipantAnswersAsync();
        Task<List<ParticipantAnswers>> GetAllAnswersByQuestionnaireId(string questionnaireId);
        Task UpdateParticipantAnswerAsync(ParticipantAnswers participantAnswer);
        Task<ParticipantAnswers?> GetParticipantAnswerAsync(string participantId, string questionnaireId, string questionText);

    }
}
