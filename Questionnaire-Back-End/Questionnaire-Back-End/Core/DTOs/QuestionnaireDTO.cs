namespace Questionnaire_Back_End.Core.DTOs
{
    public class QuestionnaireDTO
    {
        public string id { get; set; }
        public string name { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string description { get; set; }
        public string targetGroup { get; set; }
        public List<QuestionDTO> questions { get; set; } = new List<QuestionDTO>();
    }
}
