using UnityEngine;
using UnityEditor;

public class ModifyLibraryObject
{
    [MenuItem("Prefabs/Modify/Library Object")]
    static public void DontDoThis()
    {
        string path = "Assets/Prefabs/A.prefab";
        var goA = (GameObject)AssetDatabase.LoadMainAssetAtPath(path);

        GameObjectUtility.SetStaticEditorFlags(goA, ~GameObjectUtility.GetStaticEditorFlags(goA));
        PrefabUtility.SavePrefabAsset(goA);
    }

    [MenuItem("Prefabs/Modify/Library Object B inside A")]
    static public void DontDoThisAtAll()
    {
        string path = "Assets/Prefabs/A.prefab";
        var goA = (GameObject)AssetDatabase.LoadMainAssetAtPath(path);

        var goB = goA.GetComponent<Transform>().GetChild(0).gameObject;

        GameObjectUtility.SetStaticEditorFlags(goB, ~GameObjectUtility.GetStaticEditorFlags(goB));

        // B is a nested prefab inside A
        // So doing this resulsts in PropertyModifications being generate for B inside A
        PrefabUtility.SavePrefabAsset(goA);
    }
}
