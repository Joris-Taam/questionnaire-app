using Microsoft.EntityFrameworkCore;
using Questionnaire_Back_End.Core.DTOs;
using Questionnaire_Back_End.Core.Repositories;
using Questionnaire_Back_End.Core.Interfaces;
using Questionnaire_Back_End.Data.DbContext;

namespace Questionnaire_Back_End.Core.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly ITargetGroupRepository _targetGroupRepository;

        public ParticipantService(IParticipantRepository participantRepository, ITargetGroupRepository targetGroupRepository)
        {
            _participantRepository = participantRepository;
            _targetGroupRepository = targetGroupRepository;
        }

        public async Task<IEnumerable<ParticipantDTO>> GetParticipantAsync(string participantId)
        {
            var participants = await _participantRepository.GetParticipantAsync(participantId);
            return participants.Select(p => new ParticipantDTO
            {
                public_id = p.public_id,
                participant_name = p.participant_name
            });
        }

         public async Task<List<ParticipantDTO>> GetParticipantAnswersByGroupAsync(string questionnaireId)
        {
            var participantAnswers = await _participantRepository.GetAllAnswerQuestionsByGroup(questionnaireId);

            return participantAnswers
                .GroupBy(pa => pa.participant.public_id)
                .Select(group => new ParticipantDTO
                {
                    public_id = group.Key,
                    participant_name = group.First().participant.participant_name,
                    answers = group.Select(answer => new ParticipantAnswerDTO
                    {
                        participant_id = answer.participant_id,
                        questionnaire_id = answer.questionnaire_id,
                        question_text = answer.question_text,
                        option_text = answer.option_text
                    }).ToList()
                }).ToList();
        }

        public async Task<List<ParticipantAnswerDTO>> GetParticipantAnswersByParticipantId(string questionnaireId, string participantId)
        {
            var participantsAnswer = await _participantRepository.GetAllAnswerQuestionsByGroup(questionnaireId);

            return participantsAnswer
                .Where(p => p.participant_id == participantId)
                .Select(p => new ParticipantAnswerDTO
                {
                    participant_id = p.participant_id,
                    questionnaire_id = p.questionnaire_id,
                    question_text = p.question_text,
                    option_text = p.option_text
                }).ToList();
        }

        public async Task<List<TargetGroupDTO>> GetTargetGroupsAsync()
        {
            var targetGroups = await _targetGroupRepository.GetTargetGroupsAsync();

            return targetGroups.Select(tg => new TargetGroupDTO
            {
                target_group_name = tg.target_group_name
            }).ToList();
        }

        public async Task SaveTargetGroupAsync(SaveTargetGroupDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "The DTO cannot be null.");
            }

            var targetGroup = await _targetGroupRepository.GetTargetGroupByNameAsync(dto.TargetGroupName);
            if (targetGroup == null)
            {
                targetGroup = new TargetGroups
                {
                    target_group_name = dto.TargetGroupName 
                };

                await _targetGroupRepository.AddTargetGroupAsync(targetGroup);
            }

            var matchingParticipant = await _participantRepository.GetParticipantByEmailAndNameAsync(dto.Email, dto.ContactPerson);

            if (matchingParticipant != null)
            {
                matchingParticipant.user_target_group ??= new List<ParticipantGroup>();

                bool alreadyLinked = matchingParticipant.user_target_group
                    .Any(ug => ug.target_group_id == targetGroup.id);

                if (!alreadyLinked)
                {
                    matchingParticipant.user_target_group.Add(new ParticipantGroup
                    {
                        target_group_id = targetGroup.id,
                        target_group = targetGroup
                    });
                    await _participantRepository.UpdateParticipantAsync(matchingParticipant);
                }

                return;
            }

            var newParticipant = new Participants
            {
                public_id = Guid.NewGuid().ToString(),
                participant_name = dto.ContactPerson,
                participant_email = dto.Email,
                user_target_group = new List<ParticipantGroup>
        {
            new ParticipantGroup
            {
                target_group_id = targetGroup.id,
                target_group = targetGroup
            }
        }
            };

            await _participantRepository.AddParticipantAsync(newParticipant);
        }

        public async Task<List<int>> GetLinkedTargetGroupIdsAsync(string name)
        {
            var participantGroups = await _participantRepository.GetParticipantGroupsByNameAsync(name);

            if (participantGroups == null || !participantGroups.Any())
            {
                return new List<int>();
            }

            return participantGroups.Select(pg => pg.target_group_id).ToList();
        }

        public async Task RemoveTargetGroupLinkAsync(string contactPerson, string targetGroupName)
        {
            var participant = await _participantRepository.GetParticipantByNameAsync(contactPerson);
            if (participant == null) throw new Exception("Participant not found.");

            var targetGroup = await _targetGroupRepository.GetTargetGroupByNameAsync(targetGroupName);
            if (targetGroup == null) throw new Exception("Target group not found.");

            var link = participant.user_target_group
                .FirstOrDefault(ug => ug.target_group_id == targetGroup.id);

            if (link != null)
            {
                participant.user_target_group.Remove(link);
                await _participantRepository.UpdateParticipantAsync(participant);
            }
        }
        public async Task<Participants?> GetParticipantByNameAsync(string name)
        {
            return await _participantRepository.GetParticipantByNameAsync(name);
        }

    }
}
