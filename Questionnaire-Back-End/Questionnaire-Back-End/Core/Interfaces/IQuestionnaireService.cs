using Questionnaire_Back_End.Core.DTOs;

public interface IQuestionnaireService
{
    Task AddQuestionnaire(QuestionnaireDTO dto);
    Task UpdateQuestionnaire(QuestionnaireDTO dto);
    Task updateQuestion(QuestionDTO dto);
    Task<QuestionnaireDTO> GetQuestionnaireById(string id);
    Task<List<QuestionnaireDTO>> GetAllQuestionnaires();
    Task deleteQuestionnaire(string questionnaireId);
}