using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SetStatic
{
    /// The pupose of this script is to set the LightmapStatic
    /// flag on all selected Prefab assets
    [MenuItem("Prefabs/Modify/SetStatic")]
    static void SetStaticFlagOnPrefabs()
    {
        // For performance reasons we disable
        // asset importing while we modify the prefab
        AssetDatabase.StartAssetEditing();

        foreach (var obj in Selection.gameObjects)
        {
            // Skip if the object is not part of a Prefab Asset
            if (!PrefabUtility.IsPartOfPrefabAsset(obj))
                continue;

            // Skip if the file does not exists
            var path = AssetDatabase.GetAssetPath(obj);
            Debug.Log(path);
            if (string.IsNullOrEmpty(path))
                continue;

            // Load the content of the Prefab asset so we can modify it
            var assetRoot = PrefabUtility.LoadPrefabContents(path);

            GameObjectUtility.SetStaticEditorFlags(assetRoot, StaticEditorFlags.LightmapStatic);

            // Write the updated GameObject to the Prefab asset
            PrefabUtility.SaveAsPrefabAsset(assetRoot, path);

            // Clean up is important or we will leak scenes and
            // eventually run out of temporary scenes.
            PrefabUtility.UnloadPrefabContents(assetRoot);
        }

        // Reimport everything that has queued up since we disable
        // importing. This would be all the Prefab that was edited.
        AssetDatabase.StopAssetEditing();
    }
}
