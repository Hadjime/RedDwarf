using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_2 : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5;
    [SerializeField]
    bool isMoveUp = false;
    [SerializeField]
    bool isMoveDown = false;
    [SerializeField]
    bool isMoveRight = false;
    [SerializeField]
    bool isMoveLeft = false;

    Rigidbody2D rb;
    Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
