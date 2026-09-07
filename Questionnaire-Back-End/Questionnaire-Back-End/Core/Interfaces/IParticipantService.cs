using Questionnaire_Back_End.Core.DTOs;
using Questionnaire_Back_End.Data.DbContext;

namespace Questionnaire_Back_End.Core.Services
{
    public interface IParticipantService
    {

        Task<IEnumerable<ParticipantDTO>> GetParticipantAsync(string participantId);
        Task<List<ParticipantDTO>> GetParticipantAnswersByGroupAsync(string questionnaireId);
        Task<List<ParticipantAnswerDTO>> GetParticipantAnswersByParticipantId(string questionnaireId, string participantId);
        Task<List<TargetGroupDTO>> GetTargetGroupsAsync();
        Task SaveTargetGroupAsync(SaveTargetGroupDTO dto);
        Task<List<int>> GetLinkedTargetGroupIdsAsync(string name);
        Task<Participants?> GetParticipantByNameAsync(string name);
        Task RemoveTargetGroupLinkAsync(string contactPerson, string targetGroupName);
    }
}
