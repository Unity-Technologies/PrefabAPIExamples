using UnityEngine;
using UnityEditor;

public class CreatePrefab
{
    [MenuItem("Prefabs/Create/Create Prefab")]
    static public void CreateACubePrefab()
    {
        var path = EditorUtility.SaveFilePanel("Create Prefab", "Assets", "Cube.prefab", "prefab");

        if (!string.IsNullOrEmpty(path))
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
 
            // If you simply want to cube to be written as an asset but not turn in a prefab instance
            // use this method
            PrefabUtility.SaveAsPrefabAsset(cube, path);

            // This method will create a Prefab asset and make the GameObject an instance of the
            // new prefab asset
            PrefabUtility.SaveAsPrefabAssetAndConnect(cube, path, InteractionMode.AutomatedAction);
        }
    }
}
