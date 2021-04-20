using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneMicroservice
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return collection == null || collection.Count == 0;
        }

        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return array == null || array.Length == 0;
        }

        public static bool ContainsDuplicates<T>(this IList<T> list)
        {
            bool result = false;

            if (!list.IsNullOrEmpty())
            {
                result = new HashSet<T>(list).Count < list.Count;
            }

            return result;
        }

        public static bool ForAll<T>(this IList<T> list, Func<T, bool> func)
        {
            bool result = false;

            if (!list.IsNullOrEmpty())
            {
                result = true;
                for (int i = 0; i < list.Count; i++)
                {
                    if (!func(list[i]))
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        public static bool ForAll<T>(this T[] array, Func<T, bool> func)
        {
            bool result = false;

            if (!array.IsNullOrEmpty())
            {
                result = true;
                for (int i = 0; i < array.Length; i++)
                {
                    if (!func(array[i]))
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return value == null || value == string.Empty;
        }


    }


}
