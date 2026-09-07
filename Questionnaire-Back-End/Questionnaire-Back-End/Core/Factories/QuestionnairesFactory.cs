using Questionnaire_Back_End.Data.DbContext;

public static class QuestionnaireFactory
{
    public static List<Questionnaires> GetSeedData()
    {
        return new List<Questionnaires>
        {
            new Questionnaires
            {
                id = 1,
                public_id = "afe19d4a-82b0-46b5-8431-0409428dbdf8",
                questionnaire_name = "Klanttevredenheidsonderzoek",
                description = "Een vragenlijst voor klanten om hun tevredenheid te meten over onze service."
            },
            new Questionnaires
            {
                id = 2,
                public_id = "afe19d4a-82b0-34b5-8431-0409428dbdf8",
                questionnaire_name = "Productfeedback",
                description = "Een vragenlijst om feedback te verzamelen over ons nieuwste product."
            },
            new Questionnaires
            {
                id = 3,
                public_id = "d1aa8f7a-1bd2-4b65-aad9-882c31456789",
                questionnaire_name = "Personeelsbeleving",
                description = "Een interne survey om het welzijn van medewerkers te meten."
            },
            new Questionnaires
            {
                id = 4,
                public_id = "f45a02a3-9131-4d3b-9831-c3247f8ddc10",
                questionnaire_name = "Evenement Evaluatie",
                description = "Wat vond je van het laatste bedrijfsevenement? Laat het ons weten!"
            },
         
        };
    }
}
