using DLL.Context;
using DLL.Repository.Interfaces;
using Domain.Models;
using System.Data.Entity;
using System.Linq.Expressions;

namespace DLL.Repository
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(MusonHotelContext _musonHotelContext) : base(_musonHotelContext)
        {

        }

        public async Task ChangeCommentAsync(Comment newComment, int oldCommentId)
        {
            var oldComment = Entities.Find(oldCommentId);

            oldComment.Title = newComment.Title;
            oldComment.Text = newComment.Text;
            oldComment.Created = newComment.Created;
            oldComment.UserId = newComment.UserId;
            oldComment.User = newComment.User;

            base._musonHotelContext.Entry(oldComment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await base._musonHotelContext.SaveChangesAsync();
        }

        public async Task CreateAnswerAsync(Comment comment, int mainCommentId, string userId)
        {
            User user = _musonHotelContext.Users.Find(userId);
            var mainComment = Entities.Find(mainCommentId);
            comment.UserId = user.Id;
            comment.User = user;

            mainComment.Comments.Add(comment);
            base._musonHotelContext.Entry(mainComment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await base._musonHotelContext.SaveChangesAsync();

            user.Comments.Add(comment);
            base._musonHotelContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await base._musonHotelContext.SaveChangesAsync();
        }

        public async Task CreateCommentAsync(Comment comment, string userId)
        {
            User user = _musonHotelContext.Users.Find(userId);
            user.Comments.Add(comment);
            base._musonHotelContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await base._musonHotelContext.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(int remCommentId)
        {
            Entities.Remove(Entities.Find(remCommentId));
            await base._musonHotelContext.SaveChangesAsync();
        }

        public override async Task<IReadOnlyCollection<Comment>> GetAllAsync()
         => await this.Entities.Include(x => x.User).Include(x => x.Comments).ToListAsync().ConfigureAwait(false);

        public override async Task<IReadOnlyCollection<Comment>> FindByConditionAsync(Expression<Func<Comment, bool>> predicat)
         => await this.Entities.Include(x => x.User).Include(x => x.Comments)
            .Where(predicat).ToListAsync().ConfigureAwait(false);

    }
}
