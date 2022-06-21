using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFire : MonoBehaviour
{
    public float fireSpeed;
    public GameObject collisionEffect;

    private void Update()
    {
        transform.Translate(Vector2.right * fireSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health.TakeDamge(this.gameObject.GetComponent<WeaponDamage>().projectileDamage);

            Destroy(this.gameObject);
            Instantiate(collisionEffect, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
}
