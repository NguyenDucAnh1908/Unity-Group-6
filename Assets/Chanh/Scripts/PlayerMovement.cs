using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded; //tiep dat
    private float horizontalInput;
    [SerializeField] private float jumpHeight;

    private void Start()
    {
      body = GetComponent<Rigidbody2D>();
      anim =  GetComponent<Animator>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2 (horizontalInput * speed, body.velocity.y);

        //Flip player moving left and right
        if(horizontalInput > 0.01f) 
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1 , 1);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            Jump();
            

        //set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }
    

    private void Jump() 
    {
        body.velocity = new Vector2(body.velocity.x, jumpHeight);
        anim.SetTrigger("jump");
        grounded = false;
    }

    //check jump dung terrain
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag == "Terrain")
            grounded = true;
    }


    
}
