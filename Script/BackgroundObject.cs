using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObject : MonoBehaviour
{
    public float XrotSpeed = 100f;
    public float YrotSpeed = 100f;
    public float ZrotSpeed = 100f;
    void Update()
    {
        transform.Rotate(new Vector3(XrotSpeed * Time.deltaTime, YrotSpeed * Time.deltaTime, ZrotSpeed * Time.deltaTime));
    }
}
