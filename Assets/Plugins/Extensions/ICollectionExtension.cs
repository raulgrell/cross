using System.Collections.Generic;
using UnityEngine;

public static class ICollectionExtension
{
    /// <summary>
    /// Returns a new array containing values inside ICollection.
    /// </summary>
    public static I[] ToArray<I>(this ICollection<I> target)
    {
        I[] result = new I[target.Count];
        target.CopyTo(result, 0);
        return result;
    }

    /// <summary>
    /// Returns a new ICollection containing items type NI mapped using a conversion function
    /// </summary>
    public static NC ConvertUsing<I, NI, NC>(this ICollection<I> target, System.Func<I, NI> valueConverter)
        where NC : ICollection<NI>, new()
    {
        NC result = new NC();
        foreach (I value in target)
        {
            result.Add(valueConverter.Invoke(value));
        }
        return result;
    }
}
