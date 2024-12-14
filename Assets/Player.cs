using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Player : MonoBehaviour
{
  
    public float moveSpeed = 5f;
    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f; 
    private Transform playerCamera; 
    private CharacterController controller;

    private float rotationX = 0f; 
    

    void Start(){
        playerCamera = Camera.main.GetComponent<Transform>();
        controller = GetComponent<CharacterController>();
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        MovePlayer();


        LookAround();
    }


    void MovePlayer()
    {
        float moveDirectionX = Input.GetAxis("Horizontal");
        float moveDirectionZ = Input.GetAxis("Vertical");


        Vector3 move = transform.right * moveDirectionX + transform.forward * moveDirectionZ;

        controller.SimpleMove(move*moveSpeed);
       // transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }


    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");


        transform.Rotate(Vector3.up * mouseX * lookSpeedX);

   
        rotationX -= mouseY * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); 
        playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }



}
