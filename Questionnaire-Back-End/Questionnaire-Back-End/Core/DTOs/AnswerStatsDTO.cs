namespace Questionnaire_Back_End.Core.DTOs
{
    public class AnswerStatsDTO
    {
        public string QuestionText { get; set; }
        public string OptionText { get; set; }
        public int Count { get; set; }
        public double Percentage { get; set; }
    }

    public class QuestionAnswerStatsDTO
    {
        public string QuestionText { get; set; }
        public int totalAnswers {get;set;}
        public List<OptionStatsDTO> Options { get; set; }
    }

    public class OptionStatsDTO
    {
        public string OptionText { get; set; }
        public int Count { get; set; }
        public double Percentage { get; set; }
    }
}
