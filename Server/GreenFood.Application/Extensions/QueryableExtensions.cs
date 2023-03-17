using System.Linq.Expressions;

namespace GreenFood.Application.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> NotNullWhere<T,TKey>(
            this IQueryable<T> query,
            Expression<Func<T,TKey>> keySelector,
            TKey key)
        {
            if (key is null)
                return query;

            var parameter = keySelector.Parameters[0];
            var keyExpression = Expression.Constant(key, typeof(TKey));

            var comparison = Expression.Equal(Expression.Convert(keySelector.Body, typeof(TKey?)),
                Expression.Convert(keyExpression, typeof(TKey?)));

            var lambda = Expression.Lambda<Func<T,bool>>(comparison, parameter);

            return query.Where(lambda);
        }
    }
}
