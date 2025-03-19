using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] Enemy;
    [SerializeField] int maxEnemyNum = 5;
    [SerializeField] float spawnDelay = 1;

    bool spawn = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (FindObjectsOfType<Enemy>().Length + FindObjectsOfType<EliteEnemy>().Length < maxEnemyNum)
        {
            if (spawn)
            {
                StartCoroutine(Spawn());
            }
        }
    }

    IEnumerator Spawn()
    {
        int ranEne = Random.Range(1,5);
        if (ranEne == 4)
        {
            GameObject Enemies = Instantiate(Enemy[1]);
            Enemies.transform.position = gameObject.transform.position;
            spawn = false;
            yield return new WaitForSeconds(spawnDelay);
            spawn = true;
        }
        else
        {
            GameObject Enemies = Instantiate(Enemy[0]);
            Enemies.transform.position = gameObject.transform.position;
            spawn = false;
            yield return new WaitForSeconds(spawnDelay);
            spawn = true;
        }

    }
}
