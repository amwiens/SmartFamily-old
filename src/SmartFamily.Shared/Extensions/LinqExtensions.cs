﻿namespace SmartFamily.Shared.Extensions;

public static class LinqExtensions
{
    public static bool IsEmpty<T>(this IEnumerable<T>? enumerable) => enumerable == null || !enumerable.Any();

    public static void DisposeCollection<T>(this IEnumerable<T> enumerable)
        where T : IDisposable
    {
        foreach (var item in enumerable)
        {
            item.Dispose();
        }
    }

    public static IEnumerable<T> AtLeast<T>(this IEnumerable<T> enumerable, Func<T> item)
    {
        if (enumerable.IsEmpty())
        {
            return enumerable.Append(item());
        }

        return enumerable;
    }
}