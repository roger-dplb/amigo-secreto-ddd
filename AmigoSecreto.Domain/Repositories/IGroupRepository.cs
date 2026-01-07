using AmigoSecreto.Domain.Entities;
namespace AmigoSecreto.Domain.Repositories
{
    public interface IGroupRepository
    {
        Task<Group?> GetByIdAsync(int id);
        Task AddAsync(Group group);
        Task RemoveAsync(int groupId);
        Task UpdateAsync(Group group);
        Task<IEnumerable<Group>> GetByUserIdAsync(int userId);
        Task<bool> AddParticipantAsync(int groupId, int userId);
        Task<bool> RemoveParticipantAsync(int groupId, int userId);
        Task<IEnumerable<User>> GetParticipantsAsync(int groupId);
        Task<bool> AddModeratorAsync(int groupId, int userId);
        Task<bool> RemoveModeratorAsync(int groupId, int userId);
        Task<IEnumerable<User>> GetModeratorsAsync(int groupId);

    }
}