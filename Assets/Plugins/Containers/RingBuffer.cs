using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// I'll explain why we want this later
public class RingBuffer<T> : IEnumerable<T>
{
    T[] buffer;
    int start;
    int end;
    int length;

    private ArraySegment<T> ArrayOne => (start < end)
        ? new ArraySegment<T>(buffer, start, end - start)
        : new ArraySegment<T>(buffer, start, buffer.Length - start);

    private ArraySegment<T> ArrayTwo => (start < end)
        ? new ArraySegment<T>(buffer, end, 0)
        : new ArraySegment<T>(buffer, 0, end);

    public RingBuffer(int capacity)
    {
        Debug.Assert(capacity > 1);
        buffer = new T[capacity];
        start = 0;
        end = 0;
    }

    public RingBuffer(int capacity, T[] items)
    {
        Debug.Assert(capacity > 1);
        Debug.Assert(items != null);
        Debug.Assert(items.Length < capacity);
        buffer = new T[capacity];
        Array.Copy(items, buffer, items.Length);
        length = items.Length;
        start = 0;
        end = length == capacity ? 0 : length;
    }

    public void Add(T item)
    {
        buffer[end] = item;
        end = (end + 1) % buffer.Length;

        if (length == buffer.Length)
        {
            start = end;
        }
        else
        {
            length += 1;
        }
    }

    public T[] ToArray()
    {
        T[] newArray = new T[length];
        int newArrayOffset = 0;
        ArraySegment<T>[] segments = { ArrayOne, ArrayTwo };
        foreach (ArraySegment<T> segment in segments)
        {
            Debug.Assert(segment.Array != null, "segment.Array != null");
            Array.Copy(segment.Array, segment.Offset, newArray, newArrayOffset, segment.Count);
            newArrayOffset += segment.Count;
        }
        return newArray;
    }

    public IEnumerator<T> GetEnumerator()
    {
        ArraySegment<T>[] segments = { ArrayOne, ArrayTwo};
        foreach (ArraySegment<T> segment in segments)
        {
            for (int i = 0; i < segment.Count; i++)
            {
                Debug.Assert(segment.Array != null, "segment.Array != null");
                yield return segment.Array[segment.Offset + i];
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}