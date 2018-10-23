using UnityEngine;
using UnityEditor;

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

        var mainStage = UnityEditor.SceneManagement.StageUtility.GetMainStageHandle();

        // Lets determine which stage we are in first
        // because Prefab info depends on it
        var currentStage = UnityEditor.SceneManagement.StageUtility.GetStageHandle(go);
        if (currentStage == mainStage)
        {
            if (PrefabUtility.IsPartOfPrefabInstance(go))
            {
                var type = PrefabUtility.GetPrefabAssetType(go);
                Debug.Log(string.Format("GameObject is part of a Prefab Instance of type {0}" ,type));
            }
            else
            {
                Debug.Log("Selected GameObject is not part of a Prefab Instance");
            }
        }
        else
        {
            var prefabStage = UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetPrefabStage(go);
            if (prefabStage == null)
            {
                Debug.Log("Object is in an unknown stage, object information is currently not available.");
                return;
            }

            // Prefab stage
        }
    }
}
