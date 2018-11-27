using System.Linq;

namespace Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> GetPaged<T>(this IQueryable<T> data, int? skip, int? take)
        {
            var result = data;
            if (skip.HasValue)
            {
                result = result.Skip(skip.Value);
            }
            if (take.HasValue)
            {
                result = result.Take(take.Value);
            }
            return result;
        }
    }
}