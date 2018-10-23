using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteAlways]
public class AlwaysUpdate : MonoBehaviour
{
    public float rotationSpeed = 20.0f;

    [System.NonSerialized]
    public float updateCount = 0;

    void Update()
    {
        // Rotate only in playmode
        // We don't want this in edit mode nor in prefab mode
        // as it would update the prefab asset
        if (Application.IsPlaying(this))
        {
            Transform t = GetComponent<Transform>();
            t.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
        }

        ++updateCount;
        Debug.Log(string.Format("Updates {0}", updateCount));
    }
}
