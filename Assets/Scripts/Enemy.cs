using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Particles")]
    [SerializeField] GameObject Explosion;

    [Header("Enemy Stats")]
    [SerializeField] int Health = 3;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float rotateSpeed = 1000;
    [SerializeField] float CMD = 1; //Chage Motion Delay

    [Header("Random Value")]
    [SerializeField] float maxValue = 5;
    [SerializeField] float minValue = -5;

    bool changePos = false;

    GameObject Object;
    GameObject gameController;

    SceneLoader sceneLoader;

    Transform target;

    Vector2 targetPos;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Object = gameObject;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        gameController = FindObjectOfType<GameController>().gameObject;
        rb = Object.GetComponent<Rigidbody2D>();
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (changePos == false)
        {
            StartCoroutine(randomPos());
        } 
        Homing();
    }

    IEnumerator randomPos()
    {
        float xPos = target.position.x + Random.Range(minValue,maxValue);
        float yPos = target.position.y + Random.Range(minValue, maxValue);

        targetPos = new Vector2(xPos,yPos);

        changePos = true;

        yield return new WaitForSeconds(CMD);

        changePos = false;
    }

    void Homing()
    {
        Vector2 direction = targetPos - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;

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
            }
        }
        else if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        gameController.GetComponent<GameController>().IncreasePoint();
        GameObject explosion = Instantiate(Explosion) as GameObject;
        explosion.transform.position = gameObject.transform.position;
        Destroy(explosion, 0.3f);
    }
}
