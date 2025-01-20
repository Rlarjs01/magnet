using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereUpDownMove : MonoBehaviour
{
    public float low = 10f;
    public float high = 20f;
    public float delta = 0.1f;//�ӵ� ����
    public bool ckeckbox = false;

   public void OnCollisionEnter(Collision collision) {
        if (collision.transform.CompareTag("Metal") || collision.transform.CompareTag("NonMetal") ){
            collision.transform.SetParent(transform);
            ckeckbox = true;
        }
    }
    
     public void OnCollisionExit(Collision collision) {
        if (collision.transform.CompareTag("Metal") || collision.transform.CompareTag("NonMetal") ){
            collision.transform.SetParent(null);
        }
    }

    void Update()
    {
        

        float newYPosition = transform.localPosition.y + delta * Time.deltaTime;//��ֹ� �̵�
        transform.localPosition = new Vector3(transform.localPosition.x, newYPosition, transform.localPosition.z);

        if (transform.localPosition.y < low)
        {
            //delta = 0.1f;
            delta *= -1;
        }
        else if (transform.localPosition.y > high)
        {
            //delta = -0.1f;

            delta *= -1;
        }
    }
}
