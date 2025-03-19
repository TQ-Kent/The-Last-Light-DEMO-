using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteEnemy : MonoBehaviour
{
    [SerializeField] GameObject radarPrefab;
    
    [Header("Enemy Stats")] 
    [SerializeField] int Health = 3;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float rotateSpeed = 1000;

    [Header("Moving Limit")]
    [SerializeField] float minX = 0;
    [SerializeField] float maxX = 0;
    [SerializeField] float minY = 0;
    [SerializeField] float maxY = 0;

    [Header("Particles")]
    [SerializeField] GameObject Explosion;

    bool OnRadar = false;

    Transform Bullet;

    GameObject gameController;

    GameObject Object;
    GameObject radar;

    Transform target;

    Rigidbody2D rb;

    CapsuleCollider2D capsuleCollider;

    // Start is called before the first frame update
    void Start()
    {
        Object = gameObject;

        target = GameObject.FindGameObjectWithTag("Player").transform;

        capsuleCollider = Object.GetComponent<CapsuleCollider2D>();
        rb = Object.GetComponent<Rigidbody2D>();

        gameController = FindObjectOfType<GameController>().gameObject;

        radar = Instantiate(radarPrefab) as GameObject;
        radar.GetComponent<Radar>().EliteEnemy = gameObject;
        radar.transform.position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        radar.transform.position = gameObject.transform.position;
        if (!OnRadar)
        {
            Homing();
        }
        else
        {
            AvoidBullet();
        }
    }

    void Homing()
    {
        Vector2 direction = (Vector2)target.position - rb.position;

        target.position = new Vector2(Mathf.Clamp(target.position.x, minX, maxX), Mathf.Clamp(target.position.y, minY, maxY));

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;

        rb.velocity = transform.up * moveSpeed;
    }

    void AvoidBullet()
    {
        Vector2 direction = (Vector2)Bullet.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = rotateAmount * rotateSpeed;

        rb.velocity = transform.up * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LightSpark")
        {
            Health -= 1;
            if (Health <= 0)
            {
                Destroy(gameObject);
                Destroy(radar);
            }
        }
        else if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Destroy(radar);
        }
    }

    private void OnDestroy()
    {
        gameController.GetComponent<GameController>().IncreasePoint();
        GameObject explosion = Instantiate(Explosion) as GameObject;
        explosion.transform.position = gameObject.transform.position;
        Destroy(explosion, 0.3f);
    }

    public void GetBullet(Transform B)
    {
        Bullet = B;
    }

    public void BulletOnRadar(bool OR)
    {
        OnRadar = OR;
    }
}

