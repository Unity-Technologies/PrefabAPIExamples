using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogGameObjecTypeOnEvents : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Awake");
        GameObjectTypeLogging.LogGameObjectInformation(gameObject);
    }

    private void OnValidate()
    {
        Debug.Log("OnValidate");
        GameObjectTypeLogging.LogGameObjectInformation(gameObject);

    }
}
