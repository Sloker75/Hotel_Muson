using Domain.Models;
using Domain.Models.ViewModels;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<IReadOnlyCollection<User>> GetAllUserAsync();
        Task ChangeUserAsync(UserViewModel newUser, string oldUserEmail);
        Task ChangeUserPasswordAsync(User user, string currentPassword, string newPassword);
        Task<bool> RegistrationAsync(UserRegistrationViewModel userRegistrationVM);
        Task DeleteUserAsync(string remUserId);
    }
}
