using Questionnaire_Back_End.Data.DbContext;

public interface IQuestionRepository
{
    Task<Questions> GetQuestionByIdAsync(string id);
    Task<IEnumerable<Questions>> GetQuestionsByQuestionnaireIdAsync(int questionnaireId);
    Task AddAsync(Questions question);
    Task SaveAsync();

    Task Update(Questions entity);
    Task<Questions> GetByIdAsync(int questionId);
    Task<List<Questions>> GetAllAsync();
    Task Delete(string questionId);


}
