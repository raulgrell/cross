using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TaskTree", menuName = "AI/TaskTree", order = 1)]
public class TaskTree : ScriptableObject
{
    [SerializeField] List<TreeTask> m_TreeElements = new List<TreeTask>();

    internal List<TreeTask> treeElements
    {
        get => m_TreeElements;
        set => m_TreeElements = value;
    }

    void Awake()
    {
        if (m_TreeElements.Count == 0)
            m_TreeElements = MyTreeElementGenerator.GenerateRandomTree(160);
    }
}