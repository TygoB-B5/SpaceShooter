using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemymarker : MonoBehaviour
{
    private Transform player;
    private Transform marker;
    private Vector3 pos;

    private float distance;
    private float targetAppear = 60f;
    private float x, y, z, addup;
    private float beginSize = 0.1f;
    private float distantSizeMultiplier = 100;

    private SpriteRenderer marker_;

    private GameObject playerFinder;


    void Start()
    {
        //gets references to needed objects and components
        marker = GetComponent<Transform>();
        marker_ = GetComponent<SpriteRenderer>();
        playerFinder = GameObject.FindGameObjectWithTag("Player");
        player = playerFinder.GetComponent<Transform>();

    }
    void Update()
    {
        //runs every frame
        RotationLook();
        Scale();
        TestSetactiveDistance();
    }

    void RotationLook()
    {
        //locks marker rotation to player
        pos = player.transform.position - marker.transform.position;
        Quaternion lookPos = Quaternion.LookRotation(pos);
        marker.transform.rotation = Quaternion.Euler(lookPos.eulerAngles.x, lookPos.eulerAngles.y, player.transform.rotation.eulerAngles.z);
    }

    void Scale()
    {
        //makes marker bigger based on distance
        distance = Vector3.Distance(player.position, marker.position);
        marker.localScale = new Vector3(distance / distantSizeMultiplier + beginSize, distance / distantSizeMultiplier + beginSize, distance / distantSizeMultiplier + beginSize);
    }
    void TestSetactiveDistance()
    {
        //turns marker on or off based on distance
        if (distance > targetAppear)
        {
            marker_.enabled = true;
        }
        else
        {
            marker_.enabled = false;
        }
    }
}
