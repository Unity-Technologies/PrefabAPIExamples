using UnityEngine;
using UnityEditor;

public class GetInstanceHandle
{
    [MenuItem("Prefabs/Get Instance Handle")]
    static public void GetPrefabInstanceHandle()
    {
        var go = Selection.activeGameObject;
        
        if (!PrefabUtility.IsPartOfPrefabInstance(go))
        {
            Debug.Log("Selected GameObject is not a Prefab Instance");
            return;
        } 

        var handle = PrefabUtility.GetPrefabInstanceHandle(go);
    }

    [MenuItem("Prefabs/Is Selection Part of Same Prefab Instance")]
    static public void IsSelectionPartOfSamePrefabInstance()
    {
        if (Selection.gameObjects.Length < 2)
        {
            Debug.Log("Please select more than 1 GameObject");
            return;
        }

        var firstGO = Selection.gameObjects[0];
        var firstHandle = PrefabUtility.GetPrefabInstanceHandle(firstGO);

        foreach (var go in Selection.gameObjects)
        {
            var nextHandle = PrefabUtility.GetPrefabInstanceHandle(go);
            if (nextHandle != firstHandle)
            {
                Debug.Log("Selected GameObject are not part of the same Prefab Instance");
                return;
            }
        }

        if (firstHandle != null)
            Debug.Log("The selected GameObject are from the same Prefab Instance");
        else
            Debug.Log("No Prefab instance was selected");
    }
}
