using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_2 : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;
    [SerializeField]
    private bool isMoveUp = false;
    [SerializeField]
    private bool isMoveDown = false;
    [SerializeField]
    private bool isMoveRight = false;
    [SerializeField]
    private bool isMoveLeft = false;

    Rigidbody2D rb;
    Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal > 0 )
        {
            movement.x = horizontal;
            movement.y = 0;
            rb.rotation = 0;
        }
        if (horizontal < 0)
        {
            movement.x = horizontal;
            movement.y = 0;
            rb.rotation = 180;
        }
        if (vertical != 0)
        {
            movement.y = vertical;
            movement.x = 0;
            rb.rotation = 90 * vertical;
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        
    }
}
