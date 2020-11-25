using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossmarker : MonoBehaviour
{
    private Transform player;
    private Transform marker;
    private Vector3 pos;

    private float distance;
    private float targetAppear = 60f;
    private float x, y, z, addup;
    private float beginSize = 2f;
    private float distantSizeMultiplier = 100;

    private SpriteRenderer marker_;

    private GameObject playerFinder;


    void Start()
    {
        //gets references to needed objects and comopnents
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
        //locks rotation to player rotation
        pos = player.transform.position - marker.transform.position;
        Quaternion lookPos = Quaternion.LookRotation(pos);
        marker.transform.rotation = Quaternion.Euler(lookPos.eulerAngles.x, lookPos.eulerAngles.y, player.transform.rotation.eulerAngles.z);
    }

    void Scale()
    {
        //changes scale based on distance
        distance = Vector3.Distance(player.position, marker.position);
        marker.localScale = new Vector3(distance / distantSizeMultiplier + beginSize, distance / distantSizeMultiplier + beginSize, distance / distantSizeMultiplier + beginSize);
    }
    void TestSetactiveDistance()
    {
        //shows marker based on distance
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