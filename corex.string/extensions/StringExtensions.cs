using System;
using System.Collections.Generic;

namespace corex.@string.extensions
{
    public static class StringExtensions
    {
        public static T To<T>(this string input)
        {
            return (T) Convert.ChangeType(input, typeof(T));
        }
    }
}
