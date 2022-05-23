using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestetObjects : MonoBehaviour
{
    public GameObject TasksObject;
    public Transform[] objects;
    Transform[] objectsTransform;
    private void Awake()
    {
        objectsTransform = new Transform[objects.Length];

        for (int i = 0; i < objects.Length; i++)
        {
            objectsTransform[i] = objects[i];
        }
    }
    private void OnApplicationQuit()
    {
        TasksObject.SetActive(false);
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i] = objectsTransform[i];
        }
    }
}
