namespace Questionnaire_Back_End.Data.Repositories
{
    using Questionnaire_Back_End.Core.Interfaces;
    using Questionnaire_Back_End.Data.DbContext;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class QuestionRepository : IQuestionRepository
    {
        private readonly QuestionnaireDbContext _dbContext;

        public QuestionRepository(QuestionnaireDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Questions> GetQuestionByIdAsync(string publicId)
        {
            return await _dbContext.Questions
                .Include(q => q.answer_options)
                .Where(q => q.public_id == publicId && q.deleted_on == null)
                .FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<Questions>> GetQuestionsByQuestionnaireIdAsync(int questionnaireId)
        {
            return await _dbContext.Questions
                .Where(q => q.questionnaire_id == questionnaireId)
                .ToListAsync();
        }

        public async Task AddAsync(Questions question)
        {
            await _dbContext.Questions.AddAsync(question);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Questions entity)
        {
            _dbContext.Questions.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Questions> GetByIdAsync(int questionId)
        {
            return await _dbContext.Questions
                .Include(q => q.answer_options)
                .FirstOrDefaultAsync(q => q.id == questionId);
        }

        public async Task<List<Questions>> GetAllAsync()
        {
            return await _dbContext.Questions
                .Include(q => q.answer_options)
                .ToListAsync();
        }

        public async Task<List<Questions>> GetByQuestionnaireIdAsync(int questionnaireId)
        {
            return await _dbContext.Questions
                .Where(q => q.questionnaire_id == questionnaireId)
                .Include(q => q.answer_options)
                .ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task Delete(string questionId)
        {
            var question = await _dbContext.Questions
                .Where(q => q.public_id == questionId && q.deleted_on == null)
                .FirstOrDefaultAsync();

            var answerOptions = await _dbContext.AnswerOptions
                .Where(a => a.question == question && a.deleted_on == null)
                .ToListAsync();

            if (question != null)
            {
                question.deleted_on = DateTime.Now;

                foreach(var option in answerOptions)
                {
                    option.deleted_on = DateTime.Now;
                }

                await _dbContext.SaveChangesAsync();
            }
        }


    }
}