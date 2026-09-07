using Questionnaire_Back_End.Data.DbContext;

public static class ParticipantFactory
{
    public static List<Participants> GetSeedData()
    {
        return new List<Participants>
        {
            new Participants
            {
                id = 1,
                public_id = "4d64155b-9e64-4253-8dbe-47e75ed4a63a",
                participant_name = "Klaas Jan",
                participant_email = "Klaas@test.nl"
            },
            new Participants
            {
                id = 2,
                public_id = "4d64155b-9e64-4253-8dbe-47e75edasd3a",
                participant_name = "Henk de Vries",
                participant_email = "Henk@test.nl"
            },
            new Participants
            {
                id = 3,
                public_id = "a1c8f1b7-2d34-4a3e-bc5e-2a1b12345678",
                participant_name = "Sophie Hansen",
                participant_email = "sophie.jansen@test.nl"
            },
            new Participants
            {
                id = 4,
                public_id = "b9d1234a-4cde-4d2f-bb44-0f5678abcd12",
                participant_name = "Jan Willem",
                participant_email = "jan.willem@test.nl"
            },
            new Participants
            {
                id = 5,
                public_id = "c7f81da2-1a2b-4fae-a9f3-1234abcd5678",
                participant_name = "Lotte Bakker",
                participant_email = "lotte.bakker@test.nl"
            },
            new Participants
            {
                id = 6,
                public_id = "d3b941af-7654-4da3-b2f3-9876abcde321",
                participant_name = "Pieter van Dijk",
                participant_email = "pieter.vandijk@test.nl"
            },
            new Participants
            {
                id = 7,
                public_id = "e1f23dcb-8a2c-4b0d-b9fe-4567cdef8901",
                participant_name = "Emma de Boer",
                participant_email = "emma.deboer@test.nl"
            },
            new Participants
            {
                id = 8,
                public_id = "f2e13a77-0b1e-4a8e-a1c2-7890abcdef12",
                participant_name = "Bram Visser",
                participant_email = "bram.visser@test.nl"
            },
            new Participants
            {
                id = 9,
                public_id = "a2b3c4d5-e6f7-4890-8123-abcdef123456",
                participant_name = "Noa Meijer",
                participant_email = "noa.meijer@test.nl"
            },
            new Participants
            {
                id = 10,
                public_id = "f3e2d1c0-b4a9-4e87-9345-fedcba654321",
                participant_name = "Daan Kuipers",
                participant_email = "daan.kuipers@test.nl"
            }
        };
    }
}
