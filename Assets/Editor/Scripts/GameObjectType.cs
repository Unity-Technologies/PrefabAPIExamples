using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class GameObjectType
{
    [MenuItem("Prefabs/Query/Get GameObject Info")]
    static public void PrintGameObjectInfo()
    {
        var go = Selection.activeGameObject;
        if (go == null)
        {
            Debug.Log("Pleae select a GameObject");
            return;
        }

        var mainStage = StageUtility.GetMainStageHandle();

        // Lets determine which stage we are in first
        // because Prefab info depends on it
        var currentStage = StageUtility.GetStageHandle(go);
        if (currentStage == mainStage)
        {
            if (PrefabUtility.IsPartOfPrefabInstance(go))
            {
                var type = PrefabUtility.GetPrefabAssetType(go);
                var path = AssetDatabase.GetAssetPath(PrefabUtility.GetCorrespondingObjectFromSource(go));
                Debug.Log(string.Format("GameObject is part of a Prefab Instance in the MainStage and is of type: {0}. It comes from the prefab asset: {1}" , type, path));
            }
            else
            {
                Debug.Log("Selected GameObject is a plain GameObject in the MainStage");
            }
        }
        else
        {
            var prefabStage = UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetPrefabStage(go);
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
