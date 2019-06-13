// Copyright 2019 Jose Luis Rovira Martin
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace UmlFromCode
{
    public static class EnumerableUtils
    {
        public static Tuple<IEnumerable<T>, IEnumerable<T>> Split<T>(this IEnumerable<T> enumer, Func<T, bool> fun)
        {
            List<T> trueItems = new List<T>();
            List<T> falseItems = new List<T>();
            foreach (T item in enumer)
            {
                if (fun(item))
                {
                    trueItems.Add(item);
                }
                else
                {
                    falseItems.Add(item);
                }
            }
            return Tuple.Create((IEnumerable<T>) trueItems, (IEnumerable<T>) falseItems);
        }

        public static IList<IEnumerable<T>> Split<T>(this IEnumerable<T> enumer, Func<T, int> fun, int c = 0)
        {
            List<IEnumerable<T>> items = new List<IEnumerable<T>>();
            if (c > 0)
            {
                for (int i = 0; i < c; i++)
                {
                    items.Add(new List<T>());
                }
            }

            foreach (T item in enumer)
            {
                int index = fun(item);

                while (items.Count <= index)
                {
                    items.Add(new List<T>());
                }
                List<T> current = (List<T>) items[index];

                current.Add(item);
            }
            return items;
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                collection.Add(item);
            }
        }

        public static IEnumerable<T> SequenceOfOne<T>(this T item)
        {
            yield return item;
        }

        public static IEnumerable<T> NotNull<T>(this IEnumerable<T> items)
        {
            return items.Where(item => item != null);
        }

        /// <summary>
        /// Improve the stream adding new information. The result is a stream of tuples,
        /// where the first item is the original item of the stream and the second item is a
        /// new value.
        /// </summary>
        public static IEnumerable<Tuple<T, TAdd>> Improve<T, TAdd>(this IEnumerable<T> items, Func<T, TAdd> fadd)
        {
            return items.Select(item => Tuple.Create(item, fadd(item)));
        }

#if false
        public static void SplitWhere<T>(this IEnumerable<T> enumer, Func<T, bool> fun,
                                         Action<IEnumerable<T>> trueAction,
                                         Action<IEnumerable<T>> falseAction)
        {
        }

        private class SplitEnumerable<T>
        {
            private readonly IEnumerable<T> enumer;

            private HashSet<Enumerator> enumerators;

            private class Enumerable : IEnumerable<T>
            {
                public IEnumerator<T> GetEnumerator()
                {
                    throw new NotImplementedException();
                }

                IEnumerator IEnumerable.GetEnumerator()
                {
                    return this.GetEnumerator();
                }
            }

            private class Enumerator : IEnumerator<T>
            {
                public T Current => throw new NotImplementedException();

                public bool MoveNext()
                {
                    throw new NotImplementedException();
                }

                public void Reset()
                {
                    throw new NotImplementedException();
                }

                object IEnumerator.Current => this.Current;

                public void Dispose()
                {
                    throw new NotImplementedException();
                }
            }
        }
#endif
    }
}
