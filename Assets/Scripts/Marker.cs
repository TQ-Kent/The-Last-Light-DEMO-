using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        if (player.spawnMarker)
        {
            Destroy(gameObject);
            player.spawnMarker = false;
        }
    }
}
