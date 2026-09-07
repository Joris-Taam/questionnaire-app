namespace Questionnaire_Back_End.Data.Repositories
{
    using Questionnaire_Back_End.Core.Interfaces;
    using Questionnaire_Back_End.Data.DbContext;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Questionnaire_Back_End.Core.DTOs;

    public class TargetGroupRepository : ITargetGroupRepository
    {
        private readonly QuestionnaireDbContext _dbContext;

        public TargetGroupRepository(QuestionnaireDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TargetGroups?> GetTargetGroupByNameAsync(string name)
        {
            return await _dbContext.TargetGroups
                .FirstOrDefaultAsync(q => q.target_group_name == name);
        }

        //public async Task<Questions> GetByIdAsync(int questionId)
        //    {
        //        return await _dbContext.Questions
        //            .Include(q => q.answer_options)
        //            .FirstOrDefaultAsync(q => q.id == questionId);
        //    }
        public async Task<List<TargetGroups>> GetTargetGroupsAsync()
        {
            return await _dbContext.TargetGroups.ToListAsync();
        }

        public async Task AddTargetGroupAsync(TargetGroups targetGroup)
        {
            _dbContext.TargetGroups.Add(targetGroup);
            await _dbContext.SaveChangesAsync();
        }
    }
}