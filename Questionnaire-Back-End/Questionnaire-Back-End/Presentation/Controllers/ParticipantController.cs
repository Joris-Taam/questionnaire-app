using Questionnaire_Back_End.Core.DTOs;
using Questionnaire_Back_End.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Questionnaire_Back_End.Core.Interfaces;

namespace Questionnaire_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantService _participantService;

        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        [HttpGet("{participantId}")]
        public async Task<ActionResult<IEnumerable<ParticipantDTO>>> GetParticipant(string participantId)
        {
            var participants = await _participantService.GetParticipantAsync(participantId);
            return Ok(participants);
        }

        [HttpGet("by-questionnaire-group/{questionnaireId}")]
        public async Task<IActionResult> GetAnswersByGroup(string questionnaireId)
        {
            var answers = await _participantService.GetParticipantAnswersByGroupAsync(questionnaireId);
            return Ok(answers);
        }

        [HttpGet("{questionnaireId}/{participantId}")]
        public async Task<IActionResult> GetAnswerByParticipant(string questionnaireId, string participantId)
        {
            var answers = await _participantService.GetParticipantAnswersByParticipantId(questionnaireId, participantId);
            return Ok(answers);
        }

        [HttpGet("target-groups")]
        public async Task<ActionResult<List<TargetGroupDTO>>> GetTargetGroups()
        {
            var targetGroups = await _participantService.GetTargetGroupsAsync();
            return Ok(targetGroups);
        }
        [HttpPost("save-target-group")]
        public async Task<IActionResult> SaveTargetGroup([FromBody] SaveTargetGroupDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Target group data is required.");
            }

            try
            {
                await _participantService.SaveTargetGroupAsync(dto);
                return Ok(new { message = "Target group and participant saved successfully." });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while saving the target group.", error = ex.Message });
            }
        }
        [HttpGet("participant-linked-groups")]
        public async Task<IActionResult> GetParticipantGroupLinks([FromQuery] string name)
        {
            var linkedGroupIds = await _participantService.GetLinkedTargetGroupIdsAsync(name);
            if (linkedGroupIds == null || !linkedGroupIds.Any())
                return NotFound();

            return Ok(linkedGroupIds);
        }
        [HttpDelete("remove-target-group")]
        public async Task<IActionResult> RemoveTargetGroupLink([FromQuery] string contactPerson, [FromQuery] string targetGroupName)
        {
            try
            {
                await _participantService.RemoveTargetGroupLinkAsync(contactPerson, targetGroupName);
                return Ok(new { message = "Target group link removed successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while removing the target group link.", error = ex.Message });
            }
        }

    }
}