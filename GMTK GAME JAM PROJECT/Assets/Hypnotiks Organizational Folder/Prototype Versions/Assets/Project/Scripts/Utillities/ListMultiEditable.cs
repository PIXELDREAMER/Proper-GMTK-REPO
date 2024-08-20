using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
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

        set
        {
            // First, find items to add and remove
            var newItems = new HashSet<T>(value);
            var oldItems = new HashSet<T>(targetlist);

            // Determine what to remove
            foreach (var item in oldItems)
            {
                if (!newItems.Contains(item))
                {
                    toRemoveList.Add(item);
                }
            }

            // Determine what to add
            foreach (var item in newItems)
            {
                if (!oldItems.Contains(item))
                {
                    toAddList.Add(item);
                }
            }

            // Perform the updates
            UpdateList();
        }
    }

    public int Count
    {
        get
        {
            UpdateList();
            return targetlist.Count;
        }

        private set
        {

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

    public bool IsEmpty()
    {
        return TargetList.Count == 0 || TargetList == null;
    }
}
