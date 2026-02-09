using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Domain.Models;

namespace NZWalks.Application.DTOs.Common.Filtering
{
    public static class FilterHelper
    {

        public static IQueryable<T> Filter<T>(IQueryable<T> source, List<FilterParam?>? filtersParam) where T : class
        {
            if (filtersParam == null) return source;

            var param = Expression.Parameter(typeof(T), "x");
            var stringContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var stringToLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
            Expression? body = null;

            foreach (var filterParam in filtersParam)
            {
                if (filterParam == null)
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(filterParam.FilterOn)
                    || string.IsNullOrWhiteSpace(filterParam.FilterQuery))
                {
                    continue;
                }

                Expression? expressionForThis = null;

                var property = typeof(T).GetProperty(filterParam.FilterOn,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property == null)
                {
                    continue;
                }

                var propAccess = Expression.Property(param, property);
                if (property.PropertyType == typeof(string))
                {

                    try
                    {
                        // right side
                        var query = Expression.Constant(filterParam.FilterQuery);
                        var toLowerQuery = Expression.Call(query, stringToLowerMethod!);
                        // left side
                        var toLower = Expression.Call(propAccess, stringToLowerMethod!);
                        // condition 1
                        var contains = Expression.Call(toLower, stringContainsMethod!, query);

                        // right side
                        var nullValue = Expression.Constant(null, typeof(string));
                        // condition 2
                        var notNull = Expression.NotEqual(propAccess, nullValue);

                        expressionForThis = Expression.AndAlso(notNull, contains);
                    }
                    catch
                    {
                        continue;
                    }
                }
                else
                {
                    var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    try
                    {
                        var query = Convert.ChangeType(filterParam.FilterQuery, targetType);
                        var filterValue = Expression.Constant(query);
                        expressionForThis = Expression.Equal(propAccess, filterValue);
                    }
                    catch
                    {
                        continue;
                    }
                }

                if (expressionForThis == null)
                {
                    continue;
                }

                body = body == null ? expressionForThis : Expression.AndAlso(body, expressionForThis);
            }

            if (body == null) return source;

            var lambda = Expression.Lambda<Func<T, bool>>(body, param);

            return source.Where(lambda);
        }
    }
}
