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
        if (Input.GetAxisRaw("Horizontal") != 0 )
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = 0;
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            movement.y = Input.GetAxisRaw("Vertical");
            movement.x = 0;
        }
        /*movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");*/
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
