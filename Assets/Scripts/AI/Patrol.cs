using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    public float distance;
    public float startWaitTime;

    private float waitTime;
    private bool moveingRight = true;

    public Transform groundDetection;

    private Rigidbody2D rb;

    private void Start()
    {
        waitTime = startWaitTime;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CollisionDetection();
    }

    public void PatrolWithSpherCollider()
    {

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);

        if (groundInfo.collider == false)
        {
            transform.Translate(Vector2.right * 0 * Time.deltaTime);
            if (waitTime <= 0)
            {
                Flip();
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    public void CollisionDetection()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);

        if (groundInfo.collider == false)
        {
            this.gameObject.GetComponent<EnemyCtrl>().isGrounded = false;
        }
        else
        {
            this.gameObject.GetComponent<EnemyCtrl>().isGrounded = true;
        }
    }

    private void Flip()
    {
        if (moveingRight == true)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            moveingRight = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            moveingRight = true;
        }
    }
}
