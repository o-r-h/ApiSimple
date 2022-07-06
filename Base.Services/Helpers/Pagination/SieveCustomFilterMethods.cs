using System.Linq;
using Base.Domain.Entities.Sieve;
using Sieve.Services;

namespace Base.Services.Helpers.Pagination
{
    public class SieveCustomFilterMethods : ISieveCustomFilterMethods
    {
        public IQueryable<Post> IsNew(IQueryable<Post> source, string op, string[] values)
            => source.Where(p => p.LikeCount < 100 && p.CommentCount < 5);
    }
}
