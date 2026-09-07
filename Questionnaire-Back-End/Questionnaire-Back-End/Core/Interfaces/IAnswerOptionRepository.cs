using Questionnaire_Back_End.Data.DbContext;

namespace Questionnaire_Back_End.Core.Interfaces
{
    public interface IAnswerOptionRepository
    {
        Task<List<AnswerOptions>> GetAnswerOptionsByQuestionIdAsync(int questionId); 
    }
}
