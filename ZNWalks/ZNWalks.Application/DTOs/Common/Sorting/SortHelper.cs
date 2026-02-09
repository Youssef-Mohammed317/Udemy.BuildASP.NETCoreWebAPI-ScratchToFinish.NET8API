using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.Application.DTOs.Common.Sorting
{
    public static class SortHelper
    {
        public static IQueryable<T> Sort<T>(IQueryable<T> source, List<SortParam?>? sortParams) where T : class
        {
            if (sortParams == null) return source;

            var param = Expression.Parameter(typeof(T), "x");  // x
            var sourceExpression = source.Expression;

            for (int i = 0; i < sortParams.Count; i++)
            {
                var sortParam = sortParams[i];

                if (sortParam == null)
                {
                    continue;
                }
                if (string.IsNullOrWhiteSpace(sortParam.SortBy))
                {
                    continue;
                }

                var prop = typeof(T).GetProperty(sortParam.SortBy,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance); // Name

                if (prop == null)
                {
                    continue;
                }

                var propAccess = Expression.Property(param, prop); // x.Name

                var lambda = Expression.Lambda(
                    typeof(Func<,>).MakeGenericType(typeof(T), prop.PropertyType),
                    propAccess, param);

                string methodName;

                if (i == 0)
                {
                    methodName = sortParam.SortDesc ? "OrderByDescending" : "OrderBy";
                }
                else
                {
                    methodName = sortParam.SortDesc ? "ThenByDescending" : "ThenBy";
                }

                sourceExpression = Expression.Call(typeof(Queryable), methodName,
                    new Type[] { typeof(T), prop.PropertyType },
                    sourceExpression,
                    Expression.Quote(lambda));
            }

            return source.Provider.CreateQuery<T>(sourceExpression);

        }
    }
}
