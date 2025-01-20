using UnityEngine;
using SimplePhysicsToolkit;

public class ChButton : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Metal") || collision.collider.CompareTag("NonMetal"))
        {
            
           BuMagnet.attract = !BuMagnet.attract;

            Debug.Log("발판");
            

        }
       
    }
}