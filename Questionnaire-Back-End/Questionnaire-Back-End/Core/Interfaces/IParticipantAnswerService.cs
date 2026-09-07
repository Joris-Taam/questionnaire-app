using Questionnaire_Back_End.Core.DTOs;

namespace Questionnaire_Back_End.Core.Interfaces
{
    public interface IParticipantAnswerService
    {
        Task PostParticipantAnswerAsync(ParticipantAnswerDTO participantAnswerDTO);
        Task<IEnumerable<ParticipantAnswerDTO>> GetParticipantAnswersAsync();
        Task<List<QuestionAnswerStatsDTO>> GetAnswerPercentages(string questionnaireId);
    }
}
