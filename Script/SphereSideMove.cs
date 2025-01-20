using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSideMove : MonoBehaviour
{
    public float left = 10f;
    public float right = 20f;
    public float delta = 0.1f;//�ӵ� ����

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.CompareTag("Metal") || collision.transform.CompareTag("NonMetal") ){
            collision.transform.SetParent(transform);
            
        }
    }
    
     private void OnCollisionExit(Collision collision) {
        if (collision.transform.CompareTag("Metal") || collision.transform.CompareTag("NonMetal") ){
            collision.transform.SetParent(null);
        }
    }

    void Update()
    {
        float newXPosition = transform.localPosition.x + delta * Time.deltaTime;//��ֹ� �̵�
        transform.localPosition = new Vector3(newXPosition, transform.localPosition.y, transform.localPosition.z);

        if (transform.localPosition.x < left)
        {
            //delta = 0.1f;
            delta *= -1;
        }
        else if (transform.localPosition.x > right)
        {
            //delta = -0.1f;

            delta *= -1;
        }
    }
    
}
