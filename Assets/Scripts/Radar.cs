using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public GameObject EliteEnemy;


    CircleCollider2D circleCollider;
    // Start is called before the first frame update
    void Start()
    {
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!circleCollider.IsTouchingLayers(LayerMask.GetMask("LightSpark")))
        {
            EliteEnemy.GetComponent<EliteEnemy>().GetBullet(null);
            EliteEnemy.GetComponent<EliteEnemy>().BulletOnRadar(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (circleCollider.IsTouchingLayers(LayerMask.GetMask("LightSpark")))
        {
            EliteEnemy.GetComponent<EliteEnemy>().GetBullet(collision.gameObject.transform);
            EliteEnemy.GetComponent<EliteEnemy>().BulletOnRadar(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (circleCollider.IsTouchingLayers(LayerMask.GetMask("LightSpark")))
        {
            EliteEnemy.GetComponent<EliteEnemy>().GetBullet(collision.gameObject.transform);
            EliteEnemy.GetComponent<EliteEnemy>().BulletOnRadar(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!circleCollider.IsTouchingLayers(LayerMask.GetMask("LightSpark")))
        {
            EliteEnemy.GetComponent<EliteEnemy>().GetBullet(null);
            EliteEnemy.GetComponent<EliteEnemy>().BulletOnRadar(false);
        }
    }
}
