namespace Questionnaire_Back_End.Core.DTOs
{
    public class QuestionDTO
    {
        public string id { get; set; }
        public string question_text { get; set; }
        public string questionnaire_id { get; set; }
        public List<AnswerOptionDTO> answer_options { get; set; }
        public int question_number { get; set; }
        public bool required { get; set; }
        
    }
}
