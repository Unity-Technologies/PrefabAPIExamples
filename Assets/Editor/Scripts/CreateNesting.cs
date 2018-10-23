using UnityEngine;
using UnityEditor;

public class CreateNesting
{
    [MenuItem("Prefabs/Nesting/NestBInA Via Instances")]
    static public void NestBInAViaInstances()
    {
        var goA = (GameObject)AssetDatabase.LoadMainAssetAtPath("Assets/Prefabs/A.prefab");
        var goB = (GameObject)AssetDatabase.LoadMainAssetAtPath("Assets/Prefabs/B.prefab");

        var instanceA = (GameObject)PrefabUtility.InstantiatePrefab(goA);
        var instanceB = (GameObject)PrefabUtility.InstantiatePrefab(goB);

        instanceB.GetComponent<Transform>().parent = instanceA.GetComponent<Transform>();
        PrefabUtility.ApplyPrefabInstance(instanceA, InteractionMode.AutomatedAction);

        // Clean up
        GameObject.DestroyImmediate(instanceA);
    }

    [MenuItem("Prefabs/Nesting/NestBInA Via Content")]
    static public void NestBInAViaContent()
    {
        string path = "Assets/Prefabs/A.prefab";

        var goB = (GameObject)AssetDatabase.LoadMainAssetAtPath("Assets/Prefabs/B.prefab");
        var instanceB = (GameObject)PrefabUtility.InstantiatePrefab(goB);

        var root = PrefabUtility.LoadPrefabContents(path);

        instanceB.GetComponent<Transform>().parent = root.GetComponent<Transform>();
        PrefabUtility.SaveAsPrefabAsset(root, path);

        // Clean up
        PrefabUtility.UnloadPrefabContents(root);
    }
}
