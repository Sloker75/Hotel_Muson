using Domain.Models;

namespace BLL.Services.Interfaces
{
    public interface ICommentService
    {
        Task<IReadOnlyCollection<Comment>> GetAllCommentsAsync();
        Task AddCommentAsync(Comment comment, string userId);
        Task AddCommentAnswerAsync(Comment comment, int mainCommentId, string userId);
        Task RemoveCommentAsync(int remCommentId);
        Task ChangeCommentAsync(Comment newComment, int oldCommentId);
    }
}
