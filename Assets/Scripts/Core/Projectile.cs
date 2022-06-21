using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public GameObject collisionEffect;
    public float damage;

    private Transform player;
    private Vector2 target;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);
    }

    private void LateUpdate()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            Destroy(gameObject);
            Instantiate(collisionEffect, gameObject.transform.position, gameObject.transform.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health.TakeDamge(damage);
            
            Destroy(this.gameObject);
            Instantiate(collisionEffect, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
}
