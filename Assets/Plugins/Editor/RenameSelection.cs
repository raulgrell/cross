using UnityEngine;
using UnityEditor;

public class RenameSelection : ScriptableWizard
{
    public GameObject[] ObjectsToRename;
    public string newName;

    [MenuItem("Custom/Rename GameObjects")]
    static void CreateWizard()
    {
        var wizard = DisplayWizard<RenameSelection>("Rename GameObjects", "Rename", "Cancel");
        wizard.ObjectsToRename = Selection.gameObjects;

        var prefillName = Selection.gameObjects[0].transform.name;
        int spaceIndex = prefillName.IndexOf(' ');
        wizard.newName = spaceIndex > 0
            ? prefillName.Substring(0, spaceIndex)
            : prefillName;
    }

    void OnWizardCreate()
    {
        foreach (GameObject go in ObjectsToRename)
        {
            go.transform.name = newName;
        }
    }
}
