using Questionnaire_Back_End.Data.DbContext;

public static class ParticipantAnswersFactory
{
    public static List<ParticipantAnswers> GetSeedData(
     List<Questionnaires> questionnaires,
     List<Questions> questions,
     List<AnswerOptions> options,
     List<Participants> participants)
    {
        var participantAnswers = new List<ParticipantAnswers>();
        int answerId = 1;
        var rand = new Random(123);

        foreach (var participant in participants)
        {
            foreach (var question in questions)
            {
                var matchingQuestionnaire = questionnaires
                    .FirstOrDefault(q => q.id == question.questionnaire_id);

                if (matchingQuestionnaire == null) continue;

                var availableOptions = options
                    .Where(opt => opt.question_id == question.id)
                    .ToList();

                if (!availableOptions.Any()) continue;

                var selectedOption = availableOptions[rand.Next(availableOptions.Count)];

                participantAnswers.Add(new ParticipantAnswers
                {
                    id = answerId++,
                    participant_id = participant.public_id,
                    questionnaire_id = matchingQuestionnaire.public_id,
                    question_text = question.question_text,
                    option_text = selectedOption.option_text
                });
            }
        }

        return participantAnswers;
    }

}