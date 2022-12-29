using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Transform Gun;
    Animator anim;
    Rigidbody2D rb;
    public Camera currentCamera;
    public Transform pfArrow;
    public float GUN_RADIUS = 0.18f;
    public float PLAYER_SPEED = 10f;
    public float CAMERA_LERP_SPEED = 5f;

    public enum State
    {
        SHOOTING,
        PRIMING,
        CAMERA_LERPING
    }

    Transform arrow;
    State state = State.PRIMING;

    //Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Gun = transform.Find("Bow");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = movement.normalized * PLAYER_SPEED;

        switch (state)
        {
            default:
                break;
            case State.PRIMING:
                currentCamera.transform.position.Set(transform.position.x, transform.position.y, -10);
       

                Vector3 screenInput = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
                Vector2 lookAt = screenInput - transform.position;
                //float angle = Vector2.Angle(lookAt, Vector2.up);
                float angle = Mathf.Atan2(lookAt.y, lookAt.x);
                Gun.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
                Gun.localPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized * GUN_RADIUS;
                anim.SetFloat("FacingAngle", (angle * Mathf.Rad2Deg + 270) % 360);
                break;
        }
        if (Input.GetMouseButtonDown(0))
        {
            state = State.SHOOTING;
            arrow = Instantiate(pfArrow, Gun.position, Gun.rotation);
        }

    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.SHOOTING:
                Vector2 diff = arrow.position - currentCamera.transform.position;
                currentCamera.transform.position += new Vector3(diff.x, diff.y, 0) * CAMERA_LERP_SPEED * Time.deltaTime;
                break;
        }

    }

}
