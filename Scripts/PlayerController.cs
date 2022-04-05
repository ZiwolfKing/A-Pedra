using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera sceneCamera;

    public float moveSpeed;

    public float cooldownTime;

    private float nextFireTime = 0;

    public Rigidbody2D rb;

    public Weapon weapon; 

    private Vector2 moveDirection;
    private Vector2 mousePosition;

    void Start()
    {
        
    }
    void Update()
    {
        ProcessInputs();
    }


    void FixedUpdate()
    {
        move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (Time.time > nextFireTime)
        {
            if(Input.GetMouseButtonDown(0))
            {
                weapon.Fire();
                nextFireTime = Time.time + cooldownTime;
            }
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    void move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }
}
