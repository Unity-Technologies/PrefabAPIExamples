using UnityEngine;
using UnityEditor;

public class GetInstanceHandle
{
    [MenuItem("Prefabs/Query/Get Instance Handle")]
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

    [MenuItem("Prefabs/Query/Is Selection Part of Same Prefab Instance")]
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

    [MenuItem("Prefabs/Query/Are 2 GameObjects Part of same Prefab Instance")]
    static public void Are2GameObjectFromSameInstance()
    {
        if (Selection.gameObjects.Length != 2)
        {
            Debug.Log("Please select 2 GameObjects");
            return;
        }

        var firstHandle = PrefabUtility.GetPrefabInstanceHandle(Selection.gameObjects[0]);
        var secondHandle = PrefabUtility.GetPrefabInstanceHandle(Selection.gameObjects[1]);

        if (firstHandle == secondHandle)
            Debug.Log("GameObject belongs to same instance");
        else
            Debug.Log("GameObject are not from the same instance");
    }

}
