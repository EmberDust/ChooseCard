using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int count = list.Count;
        int last = count - 1;

        for (int i = 0; i < last; ++i)
        {
            int randomIndex = Random.Range(i, count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public static T GetRandomElement<T>(this IList<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static T PopRandomElement<T>(this IList<T> list)
    {
        int randomIndex = Random.Range(0, list.Count);
        T result = list[randomIndex];
        list.RemoveAt(randomIndex);

        return result;
    }

    public static void ReplaceRandomElement<T>(this IList<T> list, T replaceWith)
    {
        int randomIndex = Random.Range(0, list.Count);
        list[randomIndex] = replaceWith;
    }
}
