using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReSpawn : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Metal" )
        {
            SceneManager.LoadScene("Scean");
        }
        else if (other.gameObject.tag == "NonMetal")
        {
            SceneManager.LoadScene("Scean");
        }
    }
}
