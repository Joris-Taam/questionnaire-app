using Microsoft.EntityFrameworkCore;
using Questionnaire_Back_End.Core.Interfaces;
using Questionnaire_Back_End.Data.DbContext;
using static Questionnaire_Back_End.Data.Repositories.QuestionnaireRepository;

namespace Questionnaire_Back_End.Data.Repositories
{

    public class QuestionnaireRepository : IQuestionnaireRepository
    {
        private readonly QuestionnaireDbContext _dbContext;

        public QuestionnaireRepository(QuestionnaireDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Questionnaires entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddPublications(Publications entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddTargetGroup(TargetGroupQuestionnaire entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Questionnaires entity)
        {
            _dbContext.Questionnaires.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Questionnaires> GetByIdAsync(string id)
        {
            return await _dbContext.Questionnaires
                .Include(q => q.publication_dates)
                .Include(q => q.target_group_questionnaire)
                    .ThenInclude(tgq => tgq.target_group)
                .Include(q => q.questions
                    .Where(qu => qu.deleted_on == null).OrderBy(qu => qu.question_number))
                    .ThenInclude(qu => qu.answer_options
                        .Where(ao => ao.deleted_on == null))
                .FirstOrDefaultAsync(q => q.public_id == id);
        }


        public async Task<List<Questionnaires>> GetAllAsync()
        {
            return await _dbContext.Questionnaires
                .Where(q => q.deleted_on == null)
                .Include(q => q.publication_dates)
                .Include(q => q.questions
                    .Where(ques => ques.deleted_on == null))
                .ThenInclude(q => q.answer_options)
                .ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task Delete(string questionnaireId)
        {
            var questionnaire = await _dbContext.Questionnaires
              .Where(q => q.public_id == questionnaireId && q.deleted_on == null)
              .FirstOrDefaultAsync();



            if (questionnaire != null)
            {
                questionnaire.deleted_on = DateTime.Now;

                await _dbContext.SaveChangesAsync();
            }
        }
    }

}

