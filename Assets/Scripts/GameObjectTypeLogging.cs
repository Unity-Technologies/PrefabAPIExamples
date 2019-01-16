using System.Text;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;

static public class GameObjectTypeLogging
{
    static public void LogStageInformation(GameObject go)
    {
        // First check if input GameObject is persistent before checking what stage the GameObject is in 
        if (EditorUtility.IsPersistent(go))
        {
            if (!PrefabUtility.IsPartOfPrefabAsset(go))
            {
                Debug.Log("The GameObject is a temporary object created during import. OnValidate() is called two times with a temporary object during import: First time is when saving cloned objects to .prefab file. Second event is when reading .prefab file objects during import");
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

    static public void LogPrefabInformation(GameObject go)
    {
        StringBuilder stringBuilder = new StringBuilder();

        // First check if input GameObject is persistent before checking what stage the GameObject is in 
        if (EditorUtility.IsPersistent(go))
        {
            if (!PrefabUtility.IsPartOfPrefabAsset(go))
            {
                stringBuilder.Append("The GameObject is a temporary object created during import. OnValidate() is called two times with a temporary object during import: First time is when saving cloned objects to .prefab file. Second event is when reading .prefab file objects during import");
            }
            else
            {
                stringBuilder.Append("GameObject is part of an imported Prefab Asset (from the Library folder).\n");
                stringBuilder.AppendLine("Prefab Asset: " + GetPrefabInfoString(go));
            }

            Debug.Log(stringBuilder.ToString());
            return;
        }

        PrefabStage prefabStage = PrefabStageUtility.GetPrefabStage(go);
        if (prefabStage != null)
        {
            GameObject openPrefabThatContentsIsPartOf = AssetDatabase.LoadAssetAtPath<GameObject>(prefabStage.prefabAssetPath);
            stringBuilder.AppendFormat(
                "The GameObject is part of the Prefab contents of the Prefab Asset:\n{0}\n\n",
                GetPrefabInfoString(openPrefabThatContentsIsPartOf));
        }

        if (!PrefabUtility.IsPartOfPrefabInstance(go))
        {
            stringBuilder.Append("The GameObject is a plain GameObject (not part of a Prefab instance).\n");
        }
        else
        {
            // This is the Prefab Asset that can be applied to via the Overrides dropdown.
            GameObject outermostPrefabAssetObject = PrefabUtility.GetCorrespondingObjectFromSource(go);
            // This is the Prefab Asset that determines the icon that is shown in the Hierarchy for the nearest root.
            GameObject nearestRootPrefabAssetObject = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(go));
            // This is the Prefab Asset where the original version of the object comes from.
            GameObject originalPrefabAssetObject = PrefabUtility.GetCorrespondingObjectFromOriginalSource(go);
            stringBuilder.AppendFormat(
@"Prefab Asset of the outermost Prefab instance the input GameObject is part of is:
{0}

Prefab Asset of the nearest Prefab instance root the input GameObject is part of is:
{1}

Prefab Asset of the innermost Prefab instance the input GameObject is part of is:
{2}

Complete nesting chain from outermost to original:
",
            GetPrefabInfoString(outermostPrefabAssetObject),
            GetPrefabInfoString(nearestRootPrefabAssetObject),
            GetPrefabInfoString(originalPrefabAssetObject));

            GameObject current = outermostPrefabAssetObject;
            while (current != null)
            {
                stringBuilder.AppendLine(GetPrefabInfoString(current));
                current = PrefabUtility.GetCorrespondingObjectFromSource(current);
            }
        }

        stringBuilder.AppendLine("");

        Debug.Log(stringBuilder.ToString());
    }

    static string GetPrefabInfoString(GameObject prefabAssetGameObject)
    {
        string name = prefabAssetGameObject.transform.root.gameObject.name;
        string assetPath = AssetDatabase.GetAssetPath(prefabAssetGameObject);
        PrefabAssetType type = PrefabUtility.GetPrefabAssetType(prefabAssetGameObject);
        return string.Format("<b>{0}</b> (type: {1}) at '{2}'", name, type, assetPath);
    }
}
