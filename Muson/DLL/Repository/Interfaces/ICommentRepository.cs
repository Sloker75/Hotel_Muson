using Domain.Models;

namespace DLL.Repository.Interfaces
{
    public interface ICommentRepository
    {
        Task CreateCommentAsync(Comment comment, string userId);
        Task CreateAnswerAsync(Comment comment, int mainCommentId, string userId);
        Task ChangeCommentAsync(Comment newComment, int oldCommentId);
        Task DeleteCommentAsync(int remCommentId);
    }
}
