using Questionnaire_Back_End.Core.DTOs;

public interface IQuestionService
{
    Task deleteQuestion(string questionId);
}