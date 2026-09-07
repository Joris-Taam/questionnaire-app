using Microsoft.EntityFrameworkCore;
using Questionnaire_Back_End.Core.Interfaces;
using Questionnaire_Back_End.Data.DbContext;

namespace Questionnaire_Back_End.Data.Repositories
{
    public class AnswerOptionRepository : IAnswerOptionRepository
    {
        private readonly QuestionnaireDbContext _dbContext;

        public AnswerOptionRepository(QuestionnaireDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AnswerOptions>> GetAnswerOptionsByQuestionIdAsync(int questionId)
        {
            return await _dbContext.AnswerOptions
                .Where(a => a.question_id == questionId && a.deleted_on == null)
                .ToListAsync();
        }

    }
}
