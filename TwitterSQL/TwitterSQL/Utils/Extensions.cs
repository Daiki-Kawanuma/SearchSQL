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

        public static ObservableCollection<T> ToObservableCollection<T>(this IList<T> list)
        {
            var collection = new ObservableCollection<T>();
            foreach (var element in list)
            {
                collection.Add(element);
            }
            return collection;
        }
    }
}
