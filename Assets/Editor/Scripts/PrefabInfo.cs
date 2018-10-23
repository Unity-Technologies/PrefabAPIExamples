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

        StringBuilder stringBuilder = new StringBuilder();
        PrefabStage prefabStage = PrefabStageUtility.GetPrefabStage(go);
        if (prefabStage != null)
        {
            GameObject openPrefabThatContentsIsPartOf = AssetDatabase.LoadAssetAtPath<GameObject>(prefabStage.prefabAssetPath);
            stringBuilder.AppendFormat(
                "The selected GameObject is part of the Prefab contents of the Prefab Asset:\n{0}\n\n",
                GetPrefabInfoString(openPrefabThatContentsIsPartOf));
        }

        if (!PrefabUtility.IsPartOfPrefabInstance(go))
        {
            stringBuilder.Append("The selected GameObject is a plain GameObject (not part of a Prefab instance).\n");
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
@"Prefab Asset of the outermost Prefab instance the selected GameObject is part of is:
{0}

Prefab Asset of the nearest Prefab instance root the selected GameObject is part of is:
{1}

Prefab Asset of the innermost Prefab instance the selected GameObject is part of is:
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
