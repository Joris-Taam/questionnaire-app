using Questionnaire_Back_End.Core.DTOs;
using Questionnaire_Back_End.Data.DbContext;
using Questionnaire_Back_End.Data.Repositories;

namespace Questionnaire_Back_End.Core.Interfaces
{
    public interface ITargetGroupRepository
    {
        Task<TargetGroups?> GetTargetGroupByNameAsync(string name);
        Task<List<TargetGroups>> GetTargetGroupsAsync();
        Task AddTargetGroupAsync(TargetGroups targetGroup);

    }
}
