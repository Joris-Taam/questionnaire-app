using Questionnaire_Back_End.Core.DTOs;
using Questionnaire_Back_End.Data.DbContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Questionnaire_Back_End.Core.Repositories
{
    public interface IParticipantRepository
    {
        Task<IEnumerable<ParticipantDTO>> GetParticipantAsync(string participantId);
        Task<Participants> GetParticipantByIdAsync(string public_id);
        Task<List<ParticipantAnswers>> GetAllAnswerQuestionsByGroup(string questionnaireId);

        Task AddParticipantAsync(Participants participant);
        Task<Participants?> GetParticipantByEmailAsync(string email, string name);
        Task UpdateParticipantAsync(Participants participant);
        Task<Participants?> GetParticipantByEmailAndNameAsync(string email, string name);

        Task<List<ParticipantGroup>> GetParticipantGroupsByNameAsync(string name);
        Task<Participants?> GetParticipantByNameAsync(string name);
    }
}
