using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement; 
 
[InitializeOnLoad]
class CustomPrefabEnvironment
{
    static CustomPrefabEnvironment()
    {
        // Uncomment this to dynamically create a plane when entering Prefab Mode
        //PrefabStage.prefabStageOpened += OnPrefabStageOpened;
    }
 
    static void OnPrefabStageOpened(PrefabStage prefabStage)
    {
        Debug.Log("OnPrefabStageOpened " + prefabStage.prefabAssetPath);
 
        // Get info from the PrefabStage
        var root = prefabStage.prefabContentsRoot;
        var scene = prefabStage.scene;
        var renderer = root.GetComponent<Renderer>();
 
        // If no renderer skip our custom environment
        if (renderer == null)
            return;
 
        // Create environment plane
        var plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        SceneManager.MoveGameObjectToScene(plane, scene);
 
        // Adjust environment plane to the prefab root's lower bounds
        Bounds bounds = renderer.bounds;
        plane.transform.position = new Vector3(bounds.center.x, bounds.min.y, bounds.center.z);
    }
}