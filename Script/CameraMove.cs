using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    
    public GameObject player;
    public Renderer playerColor;
    public Material goalMaterial;

    public float offsetX = 0.0f;            // ī�޶��� x��ǥ
    public float offsetY = 2.5f;           // ī�޶��� y��ǥ
    public float offsetZ = -10.0f;          // ī�޶��� z��ǥ

    public float CameraSpeed = 10.0f;   // ī�޶� �ӵ�
    Vector3 TargetPos;                      // Ÿ���� ��ġ


    void FixedUpdate()
    {
        // Ÿ���� x, y, z ��ǥ�� ī�޶��� ��ǥ�� ���Ͽ� ī�޶��� ��ġ�� ����
        TargetPos = new Vector3(player.transform.position.x + offsetX,player.transform.position.y + offsetY,
                                player.transform.position.z + offsetZ
            );

        // ī�޶��� �������� �ε巴�� �ϴ� �Լ�(Lerp)
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * CameraSpeed);
     
     if (playerColor.material.color == goalMaterial.GetColor("_Color"))
{
    offsetX = -1f;            // ī�޶��� x��ǥ
    offsetY = -1f;           // ī�޶��� y��ǥ
    offsetZ = -10.0f;  
  
}
    
    }

}


