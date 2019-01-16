using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;

static public class GameObjectTypeLogging
{
    static public void LogGameObjectInformation(GameObject go)
    {
        // First check if input GameObject is persistent before checking what stage the GameObject is in 
        if (EditorUtility.IsPersistent(go))
        {
            if (!PrefabUtility.IsPartOfPrefabAsset(go))
            {
                // If LogGameObjectTypeInformation() is called from OnValidate() the GameObject can be part of the events from the import pipeline for the persistent objects. 
                Debug.Log("Two events are fired under OnValidate(): First time is when saving objects to .prefab file. Second event is when reading .prefab file during import");
            }
            else
            {
                Debug.Log("GameObject is part of an imported Prefab Asset (from the Library folder)");
            }
            return;
        }

        // If the GameObject is not persistent let's determine which stage we are in first because getting Prefab info depends on it
        var mainStage = StageUtility.GetMainStageHandle();
        var currentStage = StageUtility.GetStageHandle(go);
        if (currentStage == mainStage)
        {
            if (PrefabUtility.IsPartOfPrefabInstance(go))
            {
                var type = PrefabUtility.GetPrefabAssetType(go);
                var path = AssetDatabase.GetAssetPath(PrefabUtility.GetCorrespondingObjectFromSource(go));
                Debug.Log(string.Format("GameObject is part of a Prefab Instance in the MainStage and is of type: {0}. It comes from the prefab asset: {1}", type, path));
            }
            else
            {
                Debug.Log("GameObject is a plain GameObject in the MainStage");
            }
        }
        else
        {
            var prefabStage = PrefabStageUtility.GetPrefabStage(go);
            if (prefabStage != null)
            {
                if (PrefabUtility.IsPartOfPrefabInstance(go))
                {
                    var type = PrefabUtility.GetPrefabAssetType(go);
                    var nestedPrefabPath = AssetDatabase.GetAssetPath(PrefabUtility.GetCorrespondingObjectFromSource(go));
                    Debug.Log(string.Format("GameObject is in a PrefabStage. The GameObject is part of a nested Prefab Instance and is of type: {0}. The opened Prefab asset is: {1} and the nested Prefab asset is: {2}", type, prefabStage.prefabAssetPath, nestedPrefabPath));
                }
                else
                {
                    var prefabAssetRoot = AssetDatabase.LoadAssetAtPath<GameObject>(prefabStage.prefabAssetPath);
                    var type = PrefabUtility.GetPrefabAssetType(prefabAssetRoot);
                    Debug.Log(string.Format("GameObject is in a PrefabStage. The opened Prefab is of type: {0}. The GameObject comes from the prefab asset: {1}", type, prefabStage.prefabAssetPath));
                }
            }
            else if (EditorSceneManager.IsPreviewSceneObject(go))
            {
                Debug.Log("GameObject is not in the MainStage, nor in a PrefabStage. But it is in a PreviewScene so could be used for Preview rendering or other utilities.");
            }
            else
            {
                Debug.LogError("Unknown GameObject Info");
            }
        }
    }
}
