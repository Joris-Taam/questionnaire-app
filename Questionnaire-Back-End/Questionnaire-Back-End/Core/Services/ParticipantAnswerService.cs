using Questionnaire_Back_End.Core.DTOs;
using Questionnaire_Back_End.Core.Interfaces;
using Questionnaire_Back_End.Data.DbContext;

namespace Questionnaire_Back_End.Core.Services
{
    public class ParticipantAnswerService : IParticipantAnswerService
    {
        private readonly IParticipantAnswerRepository _repository;

        public ParticipantAnswerService(IParticipantAnswerRepository repository)
        {
            _repository = repository;
        }

        public async Task PostParticipantAnswerAsync(ParticipantAnswerDTO participantAnswerDTO)
        {
            var existingAnswer = await _repository.GetParticipantAnswerAsync(
                participantAnswerDTO.participant_id,
                participantAnswerDTO.questionnaire_id,
                participantAnswerDTO.question_text);

            if (existingAnswer != null)
            {
                existingAnswer.option_text = participantAnswerDTO.option_text;
                await _repository.UpdateParticipantAnswerAsync(existingAnswer);
            }
            else
            {
                var participantAnswerEntity = new ParticipantAnswers
                {
                    participant_id = participantAnswerDTO.participant_id,
                    questionnaire_id = participantAnswerDTO.questionnaire_id,
                    question_text = participantAnswerDTO.question_text,
                    option_text = participantAnswerDTO.option_text
                };
                await _repository.AddParticipantAnswerAsync(participantAnswerEntity);
            }
        }

        public async Task<IEnumerable<ParticipantAnswerDTO>> GetParticipantAnswersAsync()
        {
            var participantAnswers = await _repository.GetParticipantAnswersAsync();

            return participantAnswers.Select(pa => new ParticipantAnswerDTO
            {
                participant_id = pa.participant_id,
                questionnaire_id = pa.questionnaire_id,
                question_text = pa.question_text,
                option_text = pa.option_text
            });
        }

        public async Task<List<QuestionAnswerStatsDTO>> GetAnswerPercentages(string questionnaireId)
        {
            var participantAnswers = await _repository.GetAllAnswersByQuestionnaireId(questionnaireId);

            var result = new List<QuestionAnswerStatsDTO>();

            var groupedByQuestion = participantAnswers
                .GroupBy(a => a.question_text);

            foreach (var questionGroup in groupedByQuestion)
            {
                var totalAnswers = questionGroup.Count();

                var answerCounts = questionGroup
                    .GroupBy(a => a.option_text)
                    .Select(g => new OptionStatsDTO
                    {
                        OptionText = g.Key,
                        Count = g.Count(),
                        Percentage = Math.Round((double)g.Count() / totalAnswers * 100, 2)
                    })
                    .ToList();

                result.Add(new QuestionAnswerStatsDTO
                {
                    QuestionText = questionGroup.Key,
                    Options = answerCounts
                });
            }

            return result;
        }

    }
}