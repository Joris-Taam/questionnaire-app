using Questionnaire_Back_End.Core.DTOs;
using Questionnaire_Back_End.Core.Interfaces;
using Questionnaire_Back_End.Data.DbContext;
using Microsoft.IdentityModel.Tokens;

namespace Questionnaire_Back_End.Core.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
           
        }

        public async Task deleteQuestion(string questionId)
        {
           await _questionRepository.Delete(questionId);

        }
    }
}
