using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject Marker = null;
    [SerializeField] GameObject gameController;

    [Header("Player Stats")]
    [SerializeField] int Health = 4;
    [SerializeField] float moveSpeed = 5f;

    [Header("Moving Limit")]
    [SerializeField] float minX = 0;
    [SerializeField] float maxX = 0;
    [SerializeField] float minY = 0;
    [SerializeField] float maxY = 0;
    
    [Header("Shooting system")]
    [SerializeField] float shootDelay = 0.5f;
    [SerializeField] GameObject bullet;
    [SerializeField] AudioClip shootAudio;

    [Header("Particles")]
    [SerializeField] GameObject Explosion;

    bool isMove = false;
    bool isShoot = false;

    [HideInInspector] public bool spawnMarker = false;

    GameObject Object;
    GameObject audioListener;

    CapsuleCollider2D myCollider;

    Vector3 MousePos;
    Vector3 mousePoint;

    // Start is called before the first frame update
    void Start()
    {
        Object = gameObject;
        myCollider = GetComponent<CapsuleCollider2D>();
        audioListener = GameObject.FindWithTag("AudioListener");
        gameController.GetComponent<GameController>().HealthUpdate(Health);
    }

    // Update is called once per frame
    void Update()
    {
        #region Move
        RotateCharacter();
        GetMousePoint();

        if (isMove)
        {
            Move();
        }

        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Marker")))
        {
            spawnMarker = true;
        }
        #endregion
        if (Input.GetMouseButtonDown(1) && isShoot == false)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        Debug.Log("Shoot");
        GameObject obj = Instantiate(bullet) as GameObject;
        obj.transform.position = Object.transform.position;

        AudioSource.PlayClipAtPoint(shootAudio, audioListener.transform.position);

        isShoot = true;

        yield return new WaitForSeconds(shootDelay);

        isShoot = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Health -= 1;
            gameController.GetComponent<GameController>().HealthUpdate(Health);

            if (Health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        gameController.GetComponent<GameController>().EndGame();
        GameObject explosion = Instantiate(Explosion) as GameObject;
        explosion.transform.position = gameObject.transform.position;
        Destroy(explosion, 0.3f);
    }

    #region Move
    void Move()
    {
        mousePoint = new Vector2(Mathf.Clamp(mousePoint.x, minX, maxX), Mathf.Clamp(mousePoint.y, minY, maxY));
        Object.transform.position = Vector2.Lerp(Object.transform.position, mousePoint, moveSpeed * Time.deltaTime);
        
        if (Object.transform.position == mousePoint)
        {
            isMove = false;
        }
    }

    void GetMousePoint()
    {
        if(Input.GetMouseButton(0))
        {
            mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePoint = new Vector2(mousePoint.x, mousePoint.y);
            isMove = true;
            SpawnMarker();
        }
    }    

    void RotateCharacter()
    {
        MousePos = Input.mousePosition;
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);

        Vector2 direction = new Vector2(
            MousePos.x - gameObject.transform.position.x,
            MousePos.y - gameObject.transform.position.y);

        transform.up = direction;
    }

    void SpawnMarker()
    {
        spawnMarker = true;
        Instantiate(Marker);
        Marker.transform.position = mousePoint;
    }
    #endregion

    #region WIT
    public override bool Equals(object obj)
    {
        return obj is Player player &&
               base.Equals(obj) &&
               EqualityComparer<CapsuleCollider2D>.Default.Equals(myCollider, player.myCollider);
    }

    public override int GetHashCode()
    {
        int hashCode = -1365611309;
        hashCode = hashCode * -1521134295 + base.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<CapsuleCollider2D>.Default.GetHashCode(myCollider);
        return hashCode;
    }
    #endregion
}
