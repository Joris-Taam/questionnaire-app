using Questionnaire_Back_End.Core.DTOs;
using Questionnaire_Back_End.Core.Interfaces;
using Questionnaire_Back_End.Data.DbContext;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace Questionnaire_Back_End.Core.Services
{
    public class QuestionnaireService : IQuestionnaireService
    {
        private readonly IQuestionnaireRepository _questionnaireRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerOptionRepository _answerOptionRepository;
        private readonly ITargetGroupRepository _targetGroupRepository;

        public QuestionnaireService(IQuestionnaireRepository questionnaireRepository, IQuestionRepository questionRepository, IAnswerOptionRepository answerOptionRepository, ITargetGroupRepository targetGroupRepository)
        {
            _questionnaireRepository = questionnaireRepository;
            _questionRepository = questionRepository;
            _answerOptionRepository = answerOptionRepository;
            _targetGroupRepository = targetGroupRepository;
        }

        public async Task AddQuestionnaire(QuestionnaireDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "The questionnaire DTO cannot be null.");

            if (dto.questions == null || !dto.questions.Any())
                throw new ArgumentException("The questionnaire must contain at least one question.", nameof(dto.questions));

            if (dto.name != null)
            {
                var questionnaireEntity = new Questionnaires
                {
                    public_id = dto.id,
                    questionnaire_name = dto.name,
                    description = dto.description
                };

                await _questionnaireRepository.Add(questionnaireEntity);
                await _questionRepository.SaveAsync();

                var publicationEntity = new Publications
                {
                    questionnaire_id = questionnaireEntity.id,
                    start_date = dto.startDate,
                    end_date = dto.endDate
                };

                await _questionnaireRepository.AddPublications(publicationEntity);

                var targetGroupEntity = await _targetGroupRepository.GetTargetGroupByNameAsync(dto.targetGroup);

                var targetGroup = new TargetGroupQuestionnaire
                {
                    questionnaire_id = questionnaireEntity.id,
                    target_group_id = targetGroupEntity.id
                };

                await _questionnaireRepository.AddTargetGroup(targetGroup);


                var questionEntities = dto.questions.Select(q => new Questions
                {
                    public_id = q.id,
                    question_text = q.question_text,
                    question_number = q.question_number,
                    questionnaire_id = questionnaireEntity.id,
                    required = q.required,
                    answer_options = q.answer_options.Select(a => new AnswerOptions
                    {
                        option_text = a.option_text
                    }).ToList()
                }).ToList();

                foreach (var question in questionEntities)
                {
                    if (question.question_text.IsNullOrEmpty())
                        continue;

                    await _questionRepository.AddAsync(question);
                }

                await _questionRepository.SaveAsync();
            }
        }


        public async Task UpdateQuestionnaire(QuestionnaireDTO dto)
        {
            var existingQuestionnaire = await _questionnaireRepository.GetByIdAsync(dto.id);

            if (existingQuestionnaire == null)
                throw new Exception("Questionnaire not found.");

            existingQuestionnaire.questionnaire_name = dto.name;
            existingQuestionnaire.description = dto.description;

            var targetGroup = await _targetGroupRepository.GetTargetGroupByNameAsync(dto.targetGroup);

            if (targetGroup == null)
                throw new Exception("Target group not found.");

            existingQuestionnaire.target_group_questionnaire.Clear();
            existingQuestionnaire.target_group_questionnaire.Add(new TargetGroupQuestionnaire
            {
                target_group_id = targetGroup.id,
                questionnaire_id = existingQuestionnaire.id
            });

            if (existingQuestionnaire.publication_dates != null && existingQuestionnaire.publication_dates.Any())
            {
                var firstPublication = existingQuestionnaire.publication_dates.First();
                firstPublication.start_date = dto.startDate;
                firstPublication.end_date = dto.endDate;
            }

            if (dto.questions != null && dto.questions.Any())
            {
                foreach (var questionDto in dto.questions)
                {
                    var existingQuestion = await _questionRepository.GetQuestionByIdAsync(questionDto.id);

                    if (existingQuestion != null)
                    {
                        existingQuestion.question_number = questionDto.question_number;
                        await _questionRepository.Update(existingQuestion);
                    }
                }
            }

            await _questionnaireRepository.Update(existingQuestionnaire);
            await _questionRepository.SaveAsync();
        }


        public async Task updateQuestion(QuestionDTO dto)
        {
            if (dto.question_text == null)
                throw new ArgumentException("No question text provided.");

            var existingQuestion = await _questionRepository.GetQuestionByIdAsync(dto.id);

            if (existingQuestion != null)
            {
                var existingAnswers = await _answerOptionRepository.GetAnswerOptionsByQuestionIdAsync(existingQuestion.id);

                existingQuestion.deleted_on = DateTime.Now;
                foreach (var answer in existingAnswers)
                    answer.deleted_on = DateTime.Now;

                await _questionRepository.Update(existingQuestion);
                await _questionRepository.SaveAsync();

                var newQuestion = new Questions
                {
                    question_number = existingQuestion.question_number,
                    public_id = dto.id,
                    question_text = dto.question_text,
                    questionnaire_id = existingQuestion.questionnaire_id,
                    required = dto.required,
                    answer_options = dto.answer_options?.Select(a => new AnswerOptions
                    {
                        option_text = a.option_text
                    }).ToList()
                };

                await _questionRepository.AddAsync(newQuestion);
                await _questionRepository.SaveAsync();
            }
            else
            {
                var questionnaire = await _questionnaireRepository.GetByIdAsync(dto.questionnaire_id);
                var newQuestion = new Questions
                {
                    public_id = dto.id,
                    question_number = dto.question_number,
                    question_text = dto.question_text,
                    questionnaire_id = questionnaire.id,
                    required = dto.required,
                    answer_options = dto.answer_options?.Select(a => new AnswerOptions
                    {
                        option_text = a.option_text
                    }).ToList()
                };

                await _questionRepository.AddAsync(newQuestion);
                await _questionRepository.SaveAsync();
            }
        }

        public async Task<List<QuestionnaireDTO>> GetAllQuestionnaires()
        {
            var questionnaires = await _questionnaireRepository.GetAllAsync();

            return questionnaires.Select(q => new QuestionnaireDTO
            {
                id = q.public_id,
                name = q.questionnaire_name,
                startDate = q.publication_dates?.OrderByDescending(pd => pd.start_date).FirstOrDefault()?.start_date ?? DateTime.MinValue,
                endDate = q.publication_dates?.OrderByDescending(pd => pd.start_date).FirstOrDefault()?.end_date ?? DateTime.MinValue,
                description = q.description,
                questions = q.questions?.Select(ques => new QuestionDTO
                {
                    id = ques.public_id,
                    question_text = ques.question_text,
                    answer_options = ques.answer_options?.Select(opt => new AnswerOptionDTO
                    {
                        option_text = opt.option_text
                    }).ToList()
                }).ToList()
            }).ToList();
        }


        public async Task<QuestionnaireDTO> GetQuestionnaireById(string id)
        {
            var questionnaireEntity = await _questionnaireRepository.GetByIdAsync(id);
            var publication = questionnaireEntity?.publication_dates.FirstOrDefault();
            var targetGroup = questionnaireEntity?.target_group_questionnaire.FirstOrDefault();

            if (questionnaireEntity == null)
                return null;

            return new QuestionnaireDTO
            {
                id = questionnaireEntity.public_id,
                name = questionnaireEntity.questionnaire_name,
                targetGroup = targetGroup?.target_group?.target_group_name,
                startDate = publication.start_date,
                endDate = publication.end_date,
                description = questionnaireEntity.description,
                questions = questionnaireEntity.questions != null
                    ? questionnaireEntity.questions.Select(q => new QuestionDTO
                    {
                        id = q.public_id,
                        question_text = q.question_text,
                        required = q.required,
                        answer_options = q.answer_options != null
                            ? q.answer_options.Select(a => new AnswerOptionDTO
                            {
                                option_text = a.option_text
                            }).ToList()
                            : new List<AnswerOptionDTO>()
                    }).ToList()
                    : new List<QuestionDTO>()
            };
        }

        public async Task deleteQuestionnaire(string questionnaireId)
        {
            await _questionnaireRepository.Delete(questionnaireId);
        }
    }

}
