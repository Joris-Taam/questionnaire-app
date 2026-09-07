using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Questionnaire_Back_End.Core.DTOs;
using Questionnaire_Back_End.Core.Interfaces;
using Questionnaire_Back_End.Core.Services;

namespace Questionnaire_Back_End.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionnaireController : Controller
    {
        private readonly IQuestionnaireService _questionnaireService;
        private readonly IParticipantAnswerService _participantAnswerService;

        public QuestionnaireController(IQuestionnaireService questionnaireService, IParticipantAnswerService participantAnswerService)
        {
            _questionnaireService = questionnaireService;
            _participantAnswerService = participantAnswerService;
        }

        [HttpPost]
        public async Task<IActionResult> PostQuestionnaire([FromBody] QuestionnaireDTO dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (dto == null)
                return BadRequest("Invalid data");

            await _questionnaireService.AddQuestionnaire(dto);

            return Ok(new { message = "Questionnaire added successfully" });
        }

        [HttpPost("edit/General")]
        public async Task<IActionResult> EditGeneralQuestionnaire([FromBody] QuestionnaireDTO dto)
        {
            Console.WriteLine("Received Edit Request for General Questionnaire: " + dto.id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto == null)
                return BadRequest("Invalid data");

            Console.WriteLine("DTO received for editing questionnaire: " + System.Text.Json.JsonSerializer.Serialize(dto));

            await _questionnaireService.UpdateQuestionnaire(dto);

            return Ok(new { message = "Questionnaire updated successfully" });
        }

        [HttpPost("edit/Question")]
        public async Task<IActionResult> EditQuestion([FromBody] QuestionDTO dto)
        {
            Console.WriteLine("Received Edit Request for Question: " + dto.id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto == null)
                return BadRequest("Invalid data");

            Console.WriteLine("DTO received for editing question: " + System.Text.Json.JsonSerializer.Serialize(dto));

            await _questionnaireService.updateQuestion(dto);

            return Ok(new { message = "Question updated successfully" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionnaireById(string id)
        {
          
            var questionnaire = await _questionnaireService.GetQuestionnaireById(id);

            if (questionnaire == null)
            {
                return NotFound(new { message = "Questionnaire not found." });
            }
         
            return Ok(questionnaire);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllQuestionnaires()
        {
            List<QuestionnaireDTO> questionnaires = await _questionnaireService.GetAllQuestionnaires();

            if(questionnaires == null)
            {
                return NotFound(new { message = "Geen vragenlijsten gevonden" });
            }
            return Ok(questionnaires);
        }


        [HttpDelete("delete/{questionnaireId}")]
        public async Task<IActionResult> deleteQuestionById(string questionnaireId)
        {
            if (questionnaireId.IsNullOrEmpty())
            {
                return BadRequest("No Id received");
            }

            await _questionnaireService.deleteQuestionnaire(questionnaireId);

            return Ok(new { message = "Successfully deleted" });
        }

        [HttpGet("answerpercentages/{questionnaireId}")]
        public async Task<IActionResult> SendAnswerpercentages(string questionnaireId)
        {
            var answerPercentages = await _participantAnswerService.GetAnswerPercentages(questionnaireId);

            if(answerPercentages == null)
            {
                return NotFound(new { message = "Geen antwoorden gevonden" });
            }

            return Ok(answerPercentages);

        }
    }
}
