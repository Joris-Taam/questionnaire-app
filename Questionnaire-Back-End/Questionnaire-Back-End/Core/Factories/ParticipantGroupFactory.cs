using Questionnaire_Back_End.Data.DbContext;

public static class ParticipantGroupFactory
{
    public static List<ParticipantGroup> GetSeedData()
    {
        return new List<ParticipantGroup>
        {
            new ParticipantGroup { participant_id = 1, target_group_id = 1 },
            new ParticipantGroup { participant_id = 2, target_group_id = 2 },
            new ParticipantGroup { participant_id = 1, target_group_id = 3 },
            new ParticipantGroup { participant_id = 2, target_group_id = 4 },
        };
    }
}
