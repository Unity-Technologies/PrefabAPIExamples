using UnityEngine;
using UnityEditor;

public class GameObjectType
{
    [MenuItem("Prefabs/Get GameObject Info")]
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
            //UnityEditor.Experimental.SceneManagement.PrefabStage.
            //UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetPrefabStage;
            // Prefab stage
        }
    }
}
