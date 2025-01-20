using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimplePhysicsToolkit;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{

    private bool isReached = false;

    public float maxSpeed = 5f;
    public float power = 5f;
    public float forceGravity = 18f;
    public GameObject demoGoalUI; // demoGoal UI 요소
    Renderer playerColor;
    Rigidbody playerRigidbody;

    public AudioSource mySfx;
    public AudioClip jumpSpx;
    public Material goalMaterial;

    void Start()
    {   
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        playerColor = gameObject.GetComponent<Renderer>();
        playerColor.material.color = Color.gray;
        demoGoalUI.SetActive(false);

    }
    void Update()
    {
        move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerColor.material.color != Color.gray)
            {
                playerColor.material.color = Color.gray;
                gameObject.tag = "Metal";
                Magnet.enable = true;
                MagnetCh.enable = true;
                MagnetOFF.enable = true;
                BuMagnet.enable = true;
                Magnetbox.enable = true;
            }
            else
            {
                playerColor.material.color = Color.magenta;
                gameObject.tag = "NonMetal";
                Magnet.enable = false;
                MagnetCh.enable = false;
                MagnetOFF.enable = false;
                BuMagnet.enable = false;
                Magnetbox.enable = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene("Scean");
        }
    }
    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.down * forceGravity);
    }

    void move()
    {
        float inputX = Input.GetAxis("Horizontal");

        playerRigidbody.AddForce(inputX * power * Time.deltaTime, 0, 0);

        if (playerRigidbody.velocity.x > maxSpeed)
        {
            playerRigidbody.velocity = new Vector3(maxSpeed, playerRigidbody.velocity.y, 0);
        }
        else if (playerRigidbody.velocity.x < maxSpeed * (-1))//왼쪽
        {
            playerRigidbody.velocity = new Vector3(maxSpeed * (-1), playerRigidbody.velocity.y, 0);
        }

    }

    void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("floor")){
            Debug.Log("바닥");
            JumpSound();
        }

        else if (collision.collider.CompareTag("goal"))
        {
            playerColor.material = goalMaterial;
            Debug.Log("Goal reached!");
            isReached = true;
           StartCoroutine(DelayedSetActive(0.5f));
             
        }
    }

    public void JumpSound(){
      mySfx.PlayOneShot(jumpSpx) ;
    }

    IEnumerator DelayedSetActive(float delay)
{
    yield return new WaitForSeconds(delay);
    demoGoalUI.SetActive(true);
}
}




