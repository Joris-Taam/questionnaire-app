using Questionnaire_Back_End.Data.DbContext;

public static class QuestionFactory
{
    public static List<Questions> GetSeedQuestions(List<Questionnaires> questionnaires)
    {
        var questions = new List<Questions>();
        int questionId = 1;

        foreach (var q in questionnaires)
        {
            List<string> questionTexts = q.id switch
            {
                1 => new List<string>
            {
                "Ben je tevreden over onze klantenservice?",                
                "Werd je snel geholpen?",                                 
                "Was het personeel vriendelijk tegen je?",                 
                "Wat kunnen we verbeteren aan onze klantenservice?",     
                "Wat vond je van de communicatie met onze medewerkers?"
            },
                        2 => new List<string>
            {
                "Ben je tevreden over de kwaliteit van het product?",     
                "Was de productbeschrijving accuraat?",                    
                "Werd het product op tijd geleverd?",                      
                "Wat vond je het beste of slechtste aan het product?",      
                "Wat zou je veranderen aan het product?"                   
            },
                        3 => new List<string>
            {
                "Ben je tevreden met je werk?",                             
                "Voel je je gewaardeerd binnen het team?",                  
                "Ben je tevreden met je werk-privébalans?",               
                "Wat zou je willen veranderen aan je werkomgeving?",        
                "Wat vind je het leukste of moeilijkste aan je werk?"      
            },
                        4 => new List<string>
            {
                "Was je tevreden over de locatie van het evenement?",      
                "Was de communicatie vooraf duidelijk?",               
                "Was de organisatie van het evenement goed?",
                "Wat vond je van het evenement in het algemeen?",         
                "Wat had er beter gekund aan het evenement?"               
            },
            };



            int questionNumber = 1;

            foreach (var text in questionTexts)
            {
                string shortenedText = text.Length > 150 ? text.Substring(0, 150) : text;

                questions.Add(new Questions
                {
                    id = questionId,
                    public_id = GenerateSeededGuid(questionId).ToString(),
                    question_text = shortenedText,
                    question_number = questionNumber,
                    required = true,
                    questionnaire_id = q.id
                });

                questionId++;
                questionNumber++;
            }
        }

        return questions;
    }

    private static Guid GenerateSeededGuid(int seed)
    {
        var r = new Random(seed);
        var guid = new byte[16];
        r.NextBytes(guid);
        return new Guid(guid);
    }
}
