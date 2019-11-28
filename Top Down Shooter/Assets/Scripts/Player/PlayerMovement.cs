using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    Vector3 movement;
    Rigidbody rb;
    int floorMask;
    float camRayLenght = 100f;
    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Move(h, v);
        Turning();
    }
    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }
    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if(Physics.Raycast (camRay,out floorHit, camRayLenght, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rb.MoveRotation(newRotation);
        }
    }
}
