using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoEnd : MonoBehaviour
{
    public GameObject canvas;
    public string textName = "ABC";
    void Start()
    {
        canvas.transform.Find(textName).gameObject.SetActive(false);
    }

}
