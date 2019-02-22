using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private int state;


    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }

    public void setState(int newState)
    {
        state = newState;
    }


    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

 //       switch (state)
 //       {
 //           case 0:

                rb.velocity = new Vector3(moveHorizontal * speed, 0, moveVertical * speed);
        if (moveVertical != 0 || moveHorizontal != 0)
            transform.eulerAngles = new Vector3(0, Mathf.Atan2(-moveHorizontal, -moveVertical) * 180 / Mathf.PI, 0);
        

        //     if (rb.velocity.x != 0 && idle)
        //     {
        //  anim.SetBool("walking", true);
        //  if (rb.velocity.x > 0)
        //  {
        //      spriteRenderer.flipX = false;
        //  }
        //  else
        //     spriteRenderer.flipX = true;

        //           idle = false;


        //       }
        //     else if (!idle)
        //     {
        //         anim.SetBool("walking", false);
        //         idle = true;
        //     }
        //              break;
        //        }
        //   if (damaged)
        //   {
        //       float time = Time.deltaTime + 0.1f;
        //       if(time > Time.deltaTime + 5)
        //       {
        //           damaged = false;
        //       }

        //   }

    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        Vector3 hit = collision.contacts[0].normal;
    //        //if(hit)
    //        if (hit.y == 1)
    //        {
    //            onGround = true;
    //            jumped = false;
    //      //      state = 0;
    //        }
    //    }
    //    else if (collision.gameObject.CompareTag("Enemy") && !damaged)
    //    {
    //    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    //        //   health--;
    //        //   damaged = true;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        onGround = false;
    //  //      state = 1;
    //    }
    //}
}
