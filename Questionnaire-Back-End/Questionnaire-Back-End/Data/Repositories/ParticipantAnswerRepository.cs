using Microsoft.EntityFrameworkCore;
using Questionnaire_Back_End.Core.Interfaces;
using Questionnaire_Back_End.Data.DbContext;

namespace Questionnaire_Back_End.Data.Repositories
{
    public class ParticipantAnswerRepository : IParticipantAnswerRepository
    {
        private readonly QuestionnaireDbContext _context;

        public ParticipantAnswerRepository(QuestionnaireDbContext context)
        {
            _context = context;
        }

        public async Task AddParticipantAnswerAsync(ParticipantAnswers participantAnswer)
        {
            await _context.ParticipantAnswers.AddAsync(participantAnswer);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ParticipantAnswers>> GetParticipantAnswersAsync()
        {
            return await _context.ParticipantAnswers.ToListAsync();
        }

        public async Task<List<ParticipantAnswers>> GetAllAnswersByQuestionnaireId(string questionnaireId)
        {
            return await _context.ParticipantAnswers
                .Where(q => q.questionnaire_id == questionnaireId)
                .ToListAsync();
        }
        public async Task UpdateParticipantAnswerAsync(ParticipantAnswers participantAnswer)
        {
            _context.Entry(participantAnswer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<ParticipantAnswers?> GetParticipantAnswerAsync(string participantId, string questionnaireId, string questionText)
        {
            return await _context.ParticipantAnswers.FirstOrDefaultAsync(
                answer => answer.participant_id == participantId &&
                          answer.questionnaire_id == questionnaireId &&
                          answer.question_text == questionText);
        }

    }
}
