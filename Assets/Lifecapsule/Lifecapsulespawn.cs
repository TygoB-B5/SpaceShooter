using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifecapsulespawn : MonoBehaviour
{
    public GameObject life;
    public Transform player;
    public float farAway;
    private Vector3 newPos;
    public float secondsBetweenNextSpawn;
    void Start()
    {
        StartCoroutine("SpawnLife");
    }

    IEnumerator SpawnLife()
    {
        newPos = new Vector3(player.transform.position.x + Random.Range(-farAway, farAway), player.transform.position.y + Random.Range(-farAway, farAway), player.transform.position.z + Random.Range(-farAway, farAway));
        Instantiate(life, newPos, player.transform.rotation);
        yield return new WaitForSeconds(secondsBetweenNextSpawn);
        StartCoroutine("SpawnLife");
    }
}
