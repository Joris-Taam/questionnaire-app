namespace Questionnaire_Back_End.Core.DTOs
{
    public class ParticipantAnswerDTO
    {
        public string participant_id { get; set; }
        public string questionnaire_id { get; set; }
        public string question_text { get; set; }
        public string option_text { get; set; }
    }

}
