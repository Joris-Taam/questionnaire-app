using Questionnaire_Back_End.Data.DbContext;
using System;
using System.Collections.Generic;

public static class AnswerOptionFactory
{
    public static List<AnswerOptions> GetSeedAnswerOptions(List<Questions> questions)
    {
        return new List<AnswerOptions>
        {
            new AnswerOptions { id = 1, question_id = 1, option_text = "Ja" },
            new AnswerOptions { id = 2, question_id = 1, option_text = "Nee" },
            new AnswerOptions { id = 3, question_id = 2, option_text = "Ja" },
            new AnswerOptions { id = 4, question_id = 2, option_text = "Nee" },
            new AnswerOptions { id = 5, question_id = 3, option_text = "Ja" },
            new AnswerOptions { id = 6, question_id = 3, option_text = "Nee" },
            new AnswerOptions { id = 7, question_id = 4, option_text = "De wachttijd aan de telefoon mag korter" },
            new AnswerOptions { id = 8, question_id = 4, option_text = "Meer kennis bij medewerkers over specifieke producten zou helpen." },
            new AnswerOptions { id = 9, question_id = 4, option_text = "De reactietijd op e-mails en telefoontjes mag sneller." },
            new AnswerOptions { id = 10, question_id = 5, option_text = "Zeer vriendelijk en professioneel, ik voelde me serieus genomen." },
            new AnswerOptions { id = 11, question_id = 5, option_text = "Soms wat onduidelijk, vooral bij technische vragen" },
            new AnswerOptions { id = 12, question_id = 5, option_text = "Goede communicatie, ik werd netjes en op tijd op de hoogte gehouden." },
            new AnswerOptions { id = 13, question_id = 5, option_text = "Te gehaast en weinig uitleg, waardoor ik met vragen bleef zitten." },

            new AnswerOptions { id = 14, question_id = 6, option_text = "Ja" },
            new AnswerOptions { id = 15, question_id = 6, option_text = "Nee" },
            new AnswerOptions { id = 16, question_id = 7, option_text = "Ja" },
            new AnswerOptions { id = 17, question_id = 7, option_text = "Nee" },
            new AnswerOptions { id = 18, question_id = 8, option_text = "Ja" },
            new AnswerOptions { id = 19, question_id = 8, option_text = "Nee" },
            new AnswerOptions { id = 20, question_id = 9, option_text = "Het beste was de gebruiksvriendelijkheid, maar de batterijduur viel tegen." },
            new AnswerOptions { id = 21, question_id = 9, option_text = "Ik vond de kwaliteit van het materiaal uitstekend, maar het product was moeilijk te monteren." },
            new AnswerOptions { id = 22, question_id = 9, option_text = "De vormgeving is mooi en modern, maar het product voelt wat fragiel aan." },
            new AnswerOptions { id = 23, question_id = 9, option_text = "De installatie ging vlot, maar het werkte niet zoals ik had verwacht." },
            new AnswerOptions { id = 24, question_id = 10, option_text = "Een langere garantietermijn zou meer vertrouwen geven." },
            new AnswerOptions { id = 25, question_id = 10, option_text = "De bedieningsknoppen zouden iets intuïtiever mogen zijn." },
            new AnswerOptions { id = 26, question_id = 10, option_text = "Ik zou graag een stillere werking willen, vooral bij intensief gebruik" },
            new AnswerOptions { id = 27, question_id = 10, option_text = "Een handleiding in meerdere talen zou handig zijn." },

            new AnswerOptions { id = 28, question_id = 11, option_text = "Ja" },
            new AnswerOptions { id = 29, question_id = 11, option_text = "Nee" },
            new AnswerOptions { id = 30, question_id = 12, option_text = "Ja" },
            new AnswerOptions { id = 31, question_id = 12, option_text = "Nee" },
            new AnswerOptions { id = 32, question_id = 13, option_text = "Ja" },
            new AnswerOptions { id = 33, question_id = 13, option_text = "Nee" },
            new AnswerOptions { id = 34, question_id = 14, option_text = "Meer natuurlijk licht op kantoor zou fijn zijn." },
            new AnswerOptions { id = 35, question_id = 14, option_text = "Een rustigere plek om ongestoord te kunnen werken." },
            new AnswerOptions { id = 36, question_id = 14, option_text = "Een flexibeler werkrooster zou het werk makkelijker maken." },
            new AnswerOptions { id = 37, question_id = 14, option_text = "Meer groen in de werkruimte zou de sfeer verbeteren." },
            new AnswerOptions { id = 38, question_id = 15, option_text = "Het leukste vind ik de samenwerking met collega’s." },
            new AnswerOptions { id = 39, question_id = 15, option_text = "Het moeilijkste is het omgaan met strakke deadlines" },
            new AnswerOptions { id = 40, question_id = 15, option_text = "Het leukste is het oplossen van complexe problemen." },
            new AnswerOptions { id = 41, question_id = 15, option_text = "Het moeilijkste is het balanceren van meerdere projecten tegelijk." },

            new AnswerOptions { id = 42, question_id = 16, option_text = "Ja" },
            new AnswerOptions { id = 43, question_id = 16, option_text = "Nee" },
            new AnswerOptions { id = 44, question_id = 17, option_text = "Ja" },
            new AnswerOptions { id = 45, question_id = 17, option_text = "Nee" },
            new AnswerOptions { id = 46, question_id = 18, option_text = "Ja" },
            new AnswerOptions { id = 47, question_id = 18, option_text = "Nee" },
            new AnswerOptions { id = 48, question_id = 19, option_text = "Het was goed georganiseerd en de sfeer was prettig." },
            new AnswerOptions { id = 49, question_id = 19, option_text = "Leuke locatie en interessante sprekers, ik heb ervan genoten." },
            new AnswerOptions { id = 50, question_id = 19, option_text = "Het evenement was goed georganiseerd, maar sommige sessies waren te kort." },
            new AnswerOptions { id = 51, question_id = 19, option_text = "Ik vond de presentaties inspirerend, maar de locatie was lastig te bereiken." },
            new AnswerOptions { id = 52, question_id = 20, option_text = "De inschrijfprocedure was wat onduidelijk en rommelig." },
            new AnswerOptions { id = 53, question_id = 20, option_text = "Er hadden meer pauzes mogen zijn tussen de sessies" },
            new AnswerOptions { id = 54, question_id = 20, option_text = "De communicatie vooraf had duidelijker kunnen zijn." },
            new AnswerOptions { id = 55, question_id = 20, option_text = "Het evenement had meer interactie en praktische workshops mogen bevatten." },
        };
    }
}
