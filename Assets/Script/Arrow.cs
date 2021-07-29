using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    public float ARROW_SPEED = 10f;
    Vector2 lookDir;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        lookDir = (Vector2)Input.mousePosition - new Vector2(Screen.width, Screen.height) / 2;
        lookDir = lookDir.normalized;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);


        //print(lookDir);

        //      if (event is InputEventMouseMotion):
        //var mouse_pos = event.position

        //      var middle = screen_size/2
        //look_dir = (mouse_pos - middle).normalized()

        //      rotation = look_dir.angle()


    }
    private void FixedUpdate()
    {
        rb.velocity = lookDir * ARROW_SPEED;
    }

}
