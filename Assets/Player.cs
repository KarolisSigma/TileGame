using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class Player : MonoBehaviour
{
  
    public float moveSpeed = 5f;
    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f; 
    private Transform playerCamera; 
    private CharacterController controller;
    public int maxhearts;
    public int hearts;
    public List<Image> heartimages;
    public Sprite heartSprite;
    public Color fullheart;
    public Color emptyheart;

    private float rotationX = 0f; 
    

    void Start(){
        hearts=maxhearts;
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

    public void AddHearts(int amount){
        hearts+=amount;



        int count = heartimages.Count;
        for (int i = count-1; i >= 0; i--)
        {
            if(i<=hearts-1){
                heartimages[i].color=fullheart;
            }
            else{
                heartimages[i].color=emptyheart;
            }
        }
    }
    void MovePlayer()
    {
        float moveDirectionX = Input.GetAxis("Horizontal");
        float moveDirectionZ = Input.GetAxis("Vertical");


        Vector3 move = transform.right * moveDirectionX + transform.forward * moveDirectionZ;

        controller.SimpleMove(move*moveSpeed);
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
