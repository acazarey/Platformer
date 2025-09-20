using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D body;
    private Animator anim;
    private bool isJumping;

    private void Awake()
    {
        // Cache the Rigidbody2D once
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(-1.3f, 1.3f, 1.3f);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        }

        if (Input.GetKey(KeyCode.Space) && !isJumping)
        {
            body.velocity = new Vector2(body.velocity.x, speed);
            isJumping = true;
        }

        anim.SetBool("run", horizontalInput != 0);

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }
    
}
