using Microsoft.AspNetCore.Mvc;
using Questionnaire_Back_End.Core.DTOs;
using Questionnaire_Back_End.Core.Interfaces;

namespace Questionnaire_Back_End.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantAnswerController : ControllerBase
    {
        private readonly IParticipantAnswerService _participantAnswerService;

        public ParticipantAnswerController(IParticipantAnswerService participantAnswerService)
        {
            _participantAnswerService = participantAnswerService ?? throw new ArgumentNullException(nameof(participantAnswerService));
        }

        [HttpPost]
        public async Task<IActionResult> PostParticipantAnswer([FromBody] ParticipantAnswerDTO participantAnswerDTO)
        {
            Console.WriteLine("Received Post Request for Participant Answer");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            if (participantAnswerDTO == null)
            {
                return BadRequest("Invalid data");
            }

            Console.WriteLine("DTO received for creating participant answer: " + System.Text.Json.JsonSerializer.Serialize(participantAnswerDTO));

            try
            {
                await _participantAnswerService.PostParticipantAnswerAsync(participantAnswerDTO);

                return Ok(new { message = "Participant answer added successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while adding participant answer: " + ex.Message);

                return StatusCode(500, new { message = "An error occurred while processing the request" });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParticipantAnswerDTO>>> GetParticipantAnswers()
        {
            var answers = await _participantAnswerService.GetParticipantAnswersAsync();
            return Ok(answers);
        }
    }
}
