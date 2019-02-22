using UnityEngine;
using UnityEditor;

public class ReplaceSelection : ScriptableWizard
{
    public GameObject Prefab;
    public GameObject[] ObjectsToReplace;
    public bool KeepOriginalNames = true;

    [MenuItem("Custom/Replace GameObjects")]
    static void CreateWizard()
    {
        var replaceGameObjects = DisplayWizard<ReplaceSelection>("Replace GameObjects", "Replace");
        replaceGameObjects.ObjectsToReplace = Selection.gameObjects;
    }

    void OnWizardCreate()
    {
        foreach (GameObject go in ObjectsToReplace)
        {
            var newObject = (GameObject)PrefabUtility.InstantiatePrefab(Prefab);
            newObject.transform.SetParent(go.transform.parent, true);
            newObject.transform.localPosition = go.transform.localPosition;
            newObject.transform.localRotation = go.transform.localRotation;
            newObject.transform.localScale = go.transform.localScale;
            if (KeepOriginalNames)
                newObject.transform.name = go.transform.name;
            DestroyImmediate(go);
        }
    }
}
