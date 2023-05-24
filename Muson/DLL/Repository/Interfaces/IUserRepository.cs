using Domain.Models;
using Domain.Models.ViewModels;

namespace DLL.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task ChangeUserAsync(UserViewModel newUser, string oldUserEmail);
        Task ChangeUserPasswordAsync(User user, string currentPassword, string newPassword);
        Task<bool> RegistrationAsync(UserRegistrationViewModel user);
        Task DeleteUserAsync(string remUserId);
    }
}
