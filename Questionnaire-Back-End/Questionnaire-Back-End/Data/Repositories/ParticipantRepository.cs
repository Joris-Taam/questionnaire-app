using Questionnaire_Back_End.Core.DTOs;
using Microsoft.EntityFrameworkCore;
using Questionnaire_Back_End.Data.DbContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Questionnaire_Back_End.Core.Repositories
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly QuestionnaireDbContext _context;

        public ParticipantRepository(QuestionnaireDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ParticipantDTO>> GetParticipantAsync(string participantId)
        {
            return await _context.Participants
                .Select(p => new ParticipantDTO
                {
                    public_id = p.public_id,
                    participant_name = p.participant_name,
                }).Where(p => p.public_id == participantId)
                .ToListAsync();
        }

        public async Task<Participants> GetParticipantByIdAsync(string public_id)
        {
            return await _context.Participants
                .Include(p => p.user_answers)
                .FirstOrDefaultAsync(p => p.public_id == public_id);
        }

        public async Task<List<ParticipantAnswers>> GetAllAnswerQuestionsByGroup(string questionnaireId)
        {
            return await _context.ParticipantAnswers
                .Where(pa => pa.questionnaire_id == questionnaireId)
                .Include(pa => pa.participant)
                    .ThenInclude(p => p.user_target_group)
                        .ThenInclude(pg => pg.target_group)
                .ToListAsync();
        }
        public async Task AddParticipantAsync(Participants participant)
        {
            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();
        }
        public async Task<Participants?> GetParticipantByEmailAsync(string email, string name)
        {
            return await _context.Participants
                .Include(p => p.user_target_group)
                .FirstOrDefaultAsync(p => p.participant_email == email && p.participant_name == name);
        }

        public async Task UpdateParticipantAsync(Participants participant)
        {
            _context.Participants.Update(participant);
            await _context.SaveChangesAsync();
        }
        public async Task<Participants?> GetParticipantByEmailAndNameAsync(string email, string name)
        {
            return await _context.Participants
                .Include(p => p.user_target_group)
                .FirstOrDefaultAsync(p => p.participant_email == email && p.participant_name == name);
        }


        public async Task<List<ParticipantGroup>> GetParticipantGroupsByNameAsync(string name)
        {
            var participant = await _context.Participants
                .Include(p => p.user_target_group)
                .FirstOrDefaultAsync(p => p.participant_name == name);

            return participant?.user_target_group?.ToList() ?? new List<ParticipantGroup>();
        }
        public async Task<Participants?> GetParticipantByNameAsync(string name)
        {
            return await _context.Participants
                .Include(p => p.user_target_group)
                .FirstOrDefaultAsync(p => p.participant_name == name);
        }
    }
}