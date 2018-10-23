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

            // If you simply want the cube to be written as an asset but not turn into a prefab instance
            // use this method
            PrefabUtility.SaveAsPrefabAsset(cube, path);

            // This method will create a Prefab asset and make the GameObject an instance of the
            // new prefab asset
            PrefabUtility.SaveAsPrefabAssetAndConnect(cube, path, InteractionMode.AutomatedAction);
        }
    }

    [MenuItem("Prefabs/Create/Create Variant")]
    static public void CreatePrefabVariant()
    {
        var go = Selection.activeGameObject;
        if (go == null)
            return;

        bool isAsset = PrefabUtility.IsPartOfPrefabAsset(go);
        bool isInstance = PrefabUtility.IsPartOfPrefabInstance(go);

        if (!isAsset && !isInstance)
            return;

        string path = "";
        if (isAsset)
        {
            path = AssetDatabase.GetAssetPath(go);
            go = (GameObject)PrefabUtility.InstantiatePrefab(go); 
        }
        else if (isInstance)
        {
            go = PrefabUtility.GetOutermostPrefabInstanceRoot(go);
            path = AssetDatabase.GetAssetPath(PrefabUtility.GetCorrespondingObjectFromSource(go));
        }

        var prefabName = System.IO.Path.GetFileNameWithoutExtension(path);
        path = System.IO.Path.GetDirectoryName(path);
        PrefabUtility.SaveAsPrefabAsset(go, path + "/" + prefabName + " Variant.prefab");
    }
}
