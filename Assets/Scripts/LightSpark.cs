using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LightSpark : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotateSpeed = 200f;
    [SerializeField] float homingDelay = 0.5f;
    [SerializeField] float destroyDelay = 5;

    [Header("Particles")]
    [SerializeField] GameObject ExplosionPar;

    bool isHoming = false;

    GameObject target;
    GameObject Object;

    Rigidbody2D rb;

    Vector2 MousePos;
    // Start is called before the first frame update
    void Start()
    {
        Object = gameObject;

        target = GameObject.FindGameObjectWithTag("Enemy");

        rb = Object.GetComponent<Rigidbody2D>();

        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {

        if (!isHoming)
        {
            RotateBullet();
            Move();
            StartCoroutine(DelayHoming());
        }
        else
        {
            Homing();
        }
    }

    #region Moving
    void Homing()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Enemy");
        }

        Vector2 direction = (Vector2)target.transform.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;

        rb.velocity = transform.up * moveSpeed;
    }

    IEnumerator DelayHoming()
    {
        yield return new WaitForSeconds(homingDelay);
        isHoming = true;
        StopCoroutine(DelayHoming());
    }

    void RotateBullet()
    {
        MousePos = Input.mousePosition;
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);
        
        Vector2 direction = new Vector2(
            MousePos.x - gameObject.transform.position.x,
            MousePos.y - gameObject.transform.position.y);

        transform.up = direction;
    }

    void Move()
    {
        Object.transform.position = Vector2.Lerp(Object.transform.position, MousePos, moveSpeed * Time.deltaTime);
    }
    #endregion

    #region ColideAndDestroy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(Object);
        }
    }
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(Object);
        StopCoroutine(DestroyBullet());
    }
    private void OnDestroy()
    {
        GameObject explode = Instantiate(ExplosionPar) as GameObject;
        explode.transform.position = Object.transform.position;
        Destroy(explode, 0.3f);
    }
    #endregion
}
