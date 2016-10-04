using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ObjectBuilder2;

namespace TwitterSQL.Utils
{
    static class Extensions
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            items.ToList().ForEach(collection.Add);
        }

        /// <summary>
        /// Dynamically runs an aggregate function on the IQueryable.
        /// </summary>
        /// <param name="source">The IQueryable data source.</param>
        /// <param name="function">The name of the function to run. Can be Sum, Average, Min, Max.</param>
        /// <param name="member">The name of the property to aggregate over.</param>
        /// <returns>The value of the aggregate function run over the specified property.</returns>
        public static object Aggregate(this IQueryable source, string function, string member)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (member == null) throw new ArgumentNullException("member");

            // Properties
            PropertyInfo property = source.ElementType.GetTypeInfo().GetDeclaredProperty(member);
            ParameterExpression parameter = Expression.Parameter(source.ElementType, "s");
            Expression selector = Expression.Lambda(Expression.MakeMemberAccess(parameter, property), parameter);
            // We've tried to find an expression of the type Expression<Func<TSource, TAcc>>,
            // which is expressed as ( (TSource s) => s.Price 

            Debug.WriteLine("property: " + property);
            Debug.WriteLine("parameter:" + parameter);
            Debug.WriteLine("selector: " + selector);

            // Method
            MethodInfo aggregateMethod = typeof(Queryable).GetTypeInfo().DeclaredMethods.SingleOrDefault(
                m => m.Name == function
                    && m.ReturnType == property.PropertyType // should match the type of the property
                    && m.IsGenericMethod);

            Debug.WriteLine("aggregateMethod: " + aggregateMethod);

            // Sum, Average
            if (aggregateMethod != null)
            {
                return source.Provider.Execute(
                    Expression.Call(
                        null,
                        aggregateMethod.MakeGenericMethod(new[] { source.ElementType }),
                        new[] { source.Expression, Expression.Quote(selector) }));
            }
            // Min, Max
            else
            {
                aggregateMethod = typeof(Queryable).GetTypeInfo().DeclaredMethods.SingleOrDefault(
                    m => m.Name == function
                        && m.GetGenericArguments().Length == 2
                        && m.IsGenericMethod);

                return source.Provider.Execute(
                    Expression.Call(
                        null,
                        aggregateMethod.MakeGenericMethod(new[] { source.ElementType, property.PropertyType }),
                        new[] { source.Expression, Expression.Quote(selector) }));
            }
        }
    }
}
