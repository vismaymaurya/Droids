using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Patrol))]
public class EnemyCtrl : MonoBehaviour
{
    [SerializeField] private float experience;

    [Header("Speed Variable")]
    public float speed;
    public float retreatSpeed;

    [Header("Distance Variables")]
    public float chaseDistance;
    public float stoppingDistance;
    public float retreatDistance;

    [Header("ShootingVariables")]
    public float startTimeBtwShots;
    public GameObject projectile;
    public GameObject weapon;
    public Transform firePoint;
    private float timeBtwShots;
    public float damage;

    [Header("DropItems")]
    public GameObject[] dropItems;

    [HideInInspector]
    public bool isGrounded;

    private Transform player;
    private Patrol patrol;
    private float distance;
    private int chanceOfDrop;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        patrol = GetComponent<Patrol>();
        timeBtwShots = startTimeBtwShots;
    }

    private void Update()
    {
        Aggro();

        Physics2D.IgnoreLayerCollision(8,8);
    }

    private void Aggro()
    {
        if (player != null)
        {
            distance = Vector2.Distance(transform.position, player.position);

            float flipDistance = transform.position.x - player.position.x;

            if (distance < chaseDistance && distance > stoppingDistance)
            {
                if (isGrounded)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                    FlipWithDtection(flipDistance);
                    Shoot();
                    WeaponRotor();
                }
                else
                {
                    FlipWithDtection(flipDistance);
                    Shoot();
                    WeaponRotor();
                }

            }
            else if (distance < stoppingDistance && distance > retreatDistance)
            {
                transform.position = this.transform.position;
                Shoot();
                WeaponRotor();
            }
            else if (distance < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -retreatSpeed * Time.deltaTime);
                Shoot();
                WeaponRotor();
            }
            else
            {
                patrol.PatrolWithSpherCollider();
            }
        }
        else
        {
            patrol.PatrolWithSpherCollider();
        }
    }

    private void FlipWithDtection(float flipDistance)
    {
        if (flipDistance > 0)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else if (flipDistance < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void Shoot()
    {
        if (timeBtwShots >= startTimeBtwShots)
        {
            var inst = Instantiate(projectile, firePoint.position, Quaternion.identity);
            inst.GetComponent<Projectile>().damage = damage;
            timeBtwShots = 0;
        }
        else
        {
            timeBtwShots += Time.deltaTime;
        }
    }

    private void WeaponRotor()
    {
        Vector2 dir = player.position - weapon.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        weapon.transform.rotation = rotation;
    }

    public void Drop()
    {
        player.GetComponent<LevelSystem>().GainExperienceFlatRate(experience);
        chanceOfDrop = Random.Range(0, 3);
        Debug.Log(chanceOfDrop);
        if (chanceOfDrop == 1)
        {
            Instantiate(dropItems[0], transform.position, transform.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, retreatDistance);
    }
}
