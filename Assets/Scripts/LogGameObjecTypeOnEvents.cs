using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogGameObjecTypeOnEvents : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Awake");
        GameObjectTypeLogging.LogStageInformation(gameObject);
        //GameObjectTypeLogging.LogPrefabInfo(gameObject);
    }

    private void OnValidate()
    {
        Debug.Log("OnValidate");
        GameObjectTypeLogging.LogStageInformation(gameObject);
        //GameObjectTypeLogging.LogPrefabInfo(gameObject);
    }
}
