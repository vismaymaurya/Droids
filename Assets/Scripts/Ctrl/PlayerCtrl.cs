using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [Tooltip("Player horizontal speed")]
    public int Speed = 10;
    public float Force;
    public bool isStuck = false;

    private bool IsFacingRight = true;
    private Rigidbody2D rb;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (StaminaBar.instance.useStamina)
            {
                Hover(Force);
            }

            StaminaBar.instance.UseStamina(1);
        }

        Move();
        //Flip();
    }

    public void Move()
    {
        float playerSpeed = Input.GetAxisRaw("Horizontal") * Speed;

        if (playerSpeed != 0)
            MoveHorizontal(playerSpeed);
        else
            StopMoving();

        //For Flip
        if (playerSpeed < 0)
        {
            IsFacingRight = false;
        }
        else if (playerSpeed > 0)
        {
            IsFacingRight = true;
        }
    }

    private void MoveHorizontal(float playerSpeed)
    {
        rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
    }

    private void StopMoving()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public void Hover(float playerHover)
    {
        if (StaminaBar.instance.useStamina)
        {
            rb.velocity = new Vector2(0, playerHover);
        }
        else if (!StaminaBar.instance.useStamina)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    private void Flip()
    {
        if (IsFacingRight)
        {
            this.gameObject.transform.localScale = new Vector3(1,1,1);
        }
        else if (!IsFacingRight)
        {
            this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
