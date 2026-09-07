using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Questionnaire_Back_End.Core.DTOs;
using Questionnaire_Back_End.Core.Interfaces;

namespace Questionnaire_Back_End.Presentation.Controllers
{
    [ApiController]
    [Route("api/question")]
    public class QuestionController : Controller
    {
        private readonly IQuestionnaireService _questionnaireService;
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionnaireService questionnaireService, IQuestionService questionService)
        {
            _questionnaireService = questionnaireService;
            _questionService = questionService;
        }

        [HttpDelete("delete/{questionId}")]
        public async Task<IActionResult> deleteQuestionById(string questionId)
        {
            if(questionId.IsNullOrEmpty())
            {
                return BadRequest("No Id received");
            }

            await _questionService.deleteQuestion(questionId);

            return Ok(new { message = "Successfully deleted" });
        }
    }
}
