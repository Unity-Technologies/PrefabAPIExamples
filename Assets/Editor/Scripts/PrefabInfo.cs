using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using System.Text;

public class PrefabInfo
{
    [MenuItem("Prefabs/Query/Get Prefab Info")]
    static public void PrintPrefabInfo()
    {
        var go = Selection.activeGameObject;
        if (go == null)
        {
            Debug.Log("Please select a GameObject");
            return;
        }

        GameObjectTypeLogging.LogPrefabInformation(go);
    }

 
}
