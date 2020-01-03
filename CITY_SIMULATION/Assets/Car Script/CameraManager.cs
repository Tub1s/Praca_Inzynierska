using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject focus;
    public float x = 0f;
    public float y = 0f;
    public float z = 0f;
    // Update is called once per frame
    void Update()
    {
        transform.position = focus.transform.position + focus.transform.TransformDirection(new Vector3(x, y, z));
        transform.rotation = focus.transform.rotation;
        Camera.main.fieldOfView = 90f;
    }
}
