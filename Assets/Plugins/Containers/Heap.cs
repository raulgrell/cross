using UnityEngine;
using System.Collections;
using System;
using JetBrains.Annotations;

public interface IHeapItem<in T> : IComparable<T>
{
    int HeapIndex { get; set; }
}

public class Heap<T> where T : IHeapItem<T>
{
    readonly T[] items;
    int itemCount;

    public int Count => itemCount;

    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }

    public void Add(T item)
    {
        item.HeapIndex = itemCount;
        items[itemCount] = item;
        SortUp(item);
        itemCount++;
    }

    public T RemoveFirst()
    {
        T firstItem = items[0];
        itemCount--;
        items[0] = items[itemCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return firstItem;
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex], item);
    }

    void SortDown(T item)
    {
        while (true)
        {
            int leftIndex = item.HeapIndex * 2 + 1;
            int rightIndex = item.HeapIndex * 2 + 2;
            int swapIndex = leftIndex;
            
            if (leftIndex >= itemCount)
                return;
            
            if (rightIndex < itemCount && items[leftIndex].CompareTo(items[rightIndex]) < 0)
                swapIndex = rightIndex;

            if (item.CompareTo(items[swapIndex]) >= 0)
                return;

            Swap(item, items[swapIndex]);
        }
    }

    void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;

        while (true)
        {
            T parentItem = items[parentIndex];

            if (item.CompareTo(parentItem) <= 0)
                break;
            
            Swap(item, parentItem);
            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    void Swap(T itemA, T itemB)
    {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}