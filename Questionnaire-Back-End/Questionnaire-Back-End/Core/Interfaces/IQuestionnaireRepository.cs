using Questionnaire_Back_End.Data.DbContext;

public interface IQuestionnaireRepository
{
    public Task Add(Questionnaires entity);
    public Task AddPublications(Publications entity);
    public Task AddTargetGroup(TargetGroupQuestionnaire entity);
    //Task<Questionnaires> GetByPublicIdAsync(string publicId);
    Task Update(Questionnaires entity);
    Task<List<Questionnaires>> GetAllAsync();
    Task<Questionnaires> GetByIdAsync(string id);

    Task Delete(string questionnaireId);
}
