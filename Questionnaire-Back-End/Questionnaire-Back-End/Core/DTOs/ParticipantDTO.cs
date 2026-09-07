using Questionnaire_Back_End.Api.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Questionnaire_Back_End.Core.DTOs
{
    public class ParticipantDTO
    {
        public string public_id { get; set; }

        public string participant_name { get; set; }

        public List<ParticipantAnswerDTO> answers { get; set; }
    }
}
