using Questionnaire_Back_End.Data.DbContext;

public static class TargetGroupFactory
{
    public static List<TargetGroups> GetSeedTargetGroups()
    {
        return new List<TargetGroups>
    {
        new TargetGroups { id = 1, target_group_name = "Groep 1" },
        new TargetGroups { id = 2, target_group_name = "Groep 2" },
        new TargetGroups { id = 3, target_group_name = "Groep 3" },
        new TargetGroups { id = 4, target_group_name = "Groep 4" },
        new TargetGroups { id = 5, target_group_name = "Later kiezen" }
    };
    }
}
