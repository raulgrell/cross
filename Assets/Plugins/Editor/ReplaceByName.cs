using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class ReplaceByName : ScriptableWizard
{
    public GameObject target;
    public string m_name = "";
    public bool m_matchCase = true;

    [MenuItem("Custom/Batch Replace By Name")]

    static void CreateWizard()
    {
        var replaceGameObjects = DisplayWizard<ReplaceByName>("Replace GameObjects", "Replace");
        
        //Prefill the name field with the active object
        replaceGameObjects.m_name = Selection.activeObject.name;
    }

    void OnWizardCreate()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        List<GameObject> matches = new List<GameObject>();

        foreach (GameObject g in allObjects)
        {
            if ((m_matchCase ? g.name : g.name.ToUpper()) == m_name)
            {
                matches.Add(g);
            }
        }

        if (!m_matchCase)
            m_name = m_name.ToUpper();

        for (int i = matches.Count - 1; i >= 0; i--)
        {
            GameObject newObject = PrefabUtility.InstantiatePrefab(target) as GameObject;
            newObject.transform.parent = matches[i].transform.parent;
            newObject.transform.localPosition = matches[i].transform.localPosition;
            newObject.transform.localRotation = matches[i].transform.localRotation;
            newObject.transform.localScale = matches[i].transform.localScale;
            newObject.name = matches[i].name;
            DestroyImmediate(matches[i]);
            matches.RemoveAt(i);
        }
    }
}