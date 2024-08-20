using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListMultiEditable<T> where T : Component
{
    private readonly List<T> targetlist = new ();
    private readonly List<T> toAddList = new ();
    private readonly List<T> toRemoveList = new ();

    public List<T> TargetList
    {
        get
        {
            UpdateList();
            return targetlist;
        }
    }

    public int Count
    {
        get
        {
            UpdateList();
            return targetlist.Count;
        }
    }

    private void UpdateList()
    {
        foreach (var item in toRemoveList)
        {
            targetlist.Remove(item);
        }

        targetlist.AddRange(toAddList);

        toAddList.Clear();
        toRemoveList.Clear();
    }

    public void Add(T item)
    {
        toAddList.Add(item);
    }

    public void Remove(T item)
    {
        toRemoveList.Add(item);
    }
}
