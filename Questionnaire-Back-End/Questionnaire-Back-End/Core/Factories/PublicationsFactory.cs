using System;
using System.Collections.Generic;
using Questionnaire_Back_End.Data.DbContext;

namespace Questionnaire_Back_End.Data.Factories
{
    public class PublicationsFactory
    {
        public static Publications CreatePublication(int id, Questionnaires questionnaire, DateTime startDate, DateTime endDate)
        {
            return new Publications
            {
                id = id,
                questionnaire_id = questionnaire.id,
                start_date = startDate,
                end_date = endDate
            };
        }

        public static List<Publications> GetSeedPublications(List<Questionnaires> questionnaires)
        {
            var publications = new List<Publications>();
            int publicationId = 1;

            foreach (var questionnaire in questionnaires)
            {
                DateTime startDate;
                DateTime endDate;

                if (publicationId <= 3)
                {
                    startDate = DateTime.Today; 
                    endDate = DateTime.Today.AddDays(21).AddHours(23).AddMinutes(59);
                }
                else 
                {
                    startDate = DateTime.Today.AddDays(-5); 
                    endDate = DateTime.Today.AddDays(-1).AddHours(23).AddMinutes(59);
                }

                publications.Add(CreatePublication(publicationId++, questionnaire, startDate, endDate));
            }

            return publications;
        }
    }
}
