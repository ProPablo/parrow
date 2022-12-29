using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeMovement : MonoBehaviour
{
    Rigidbody2D rb;
    //Collider2D col;
    CapsuleCollider2D col;
    public float PLAYER_SPEED = 10f;
    public float FEET = .1f;
    public float JUMP = 10f;
    public LayerMask GroundLayerMask;

    Vector2 input;
    //Store colision normal
    bool isGrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * JUMP, ForceMode2D.Impulse);
        }

    }
    private void FixedUpdate()
    {
        //if grounded do not apply gravity
        rb.velocity += input.normalized * PLAYER_SPEED * Time.deltaTime;
        isGroundedCheck();

    }
    bool isGroundedCheck()
    {
        //Do left and right grund checks for square collider (capsule only ONE ground check needed)
        Vector2 rayOrigin = col.bounds.min;
        rayOrigin.x = col.bounds.center.x;
        print(rayOrigin);
        Debug.DrawLine(rayOrigin, rayOrigin + Vector2.down * FEET, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, FEET, GroundLayerMask);
        if (hit)
        {
            isGrounded = true;
            return true;
        }
        isGrounded = false;
        return false;
    }
}
