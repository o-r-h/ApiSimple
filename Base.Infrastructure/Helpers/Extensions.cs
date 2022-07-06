using System;
using System.Linq;
using System.Linq.Expressions;

namespace Base.Infrastructure.Helpers
{
    public static class Extensions
    {
        public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> query, string propertyName, string order)
        {
            if (string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(order))
            {
                return query;
            }

            try
            {
                var param = Expression.Parameter(typeof(T), string.Empty);
                var property = Expression.PropertyOrField(param, propertyName);
                var sort = Expression.Lambda(property, param);
                var call = Expression.Call(
                    typeof(Queryable),
                    "OrderBy" + (order == "desc" ? "Descending" : string.Empty),
                    new[] { typeof(T), property.Type },
                    query.Expression,
                    Expression.Quote(sort)
                );
                query = query.Provider.CreateQuery<T>(call);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return query;
        }
    }
}
