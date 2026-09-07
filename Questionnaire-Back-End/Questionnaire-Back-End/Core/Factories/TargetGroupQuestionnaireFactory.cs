using Questionnaire_Back_End.Data.DbContext;

public static class TargetGroupQuestionnaireFactory
{
    public static List<TargetGroupQuestionnaire> GetSeedData(List<Questionnaires> questionnaires)
    {
        return new List<TargetGroupQuestionnaire>
        {
            new TargetGroupQuestionnaire { target_group_id = 1, questionnaire_id = questionnaires[0].id },
            new TargetGroupQuestionnaire { target_group_id = 2, questionnaire_id = questionnaires[1].id },
            new TargetGroupQuestionnaire { target_group_id = 3, questionnaire_id = questionnaires[2].id },
            new TargetGroupQuestionnaire { target_group_id = 4, questionnaire_id = questionnaires[3].id },
        };
    }
}
