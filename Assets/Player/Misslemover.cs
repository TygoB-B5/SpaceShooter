using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misslemover : MonoBehaviour
{
    private GameObject closestEnemy;
    float t;
    private bool trig = true;
    void FindClosestTarget()
    {
        //gets closest enemy gameobject
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        GameObject closest = null;

        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        closestEnemy = closest;
        
    }

    void Start()
    {
        //sets missle to right fire rotation
        transform.Rotate(0, 180, 0);
        t = 0;
    }

    void FixedUpdate()
    {
        //finds closest target after 0.4 sec
        if(t > 0.4f && trig == true)
        {
            FindClosestTarget();
            trig = false;
        }

        t += 1 * Time.deltaTime;

        //moves missle to closest enemy
        if (t > 0.5f)
        {
            transform.Translate(Vector3.forward * 2f);
            Destroy(gameObject, 5);

            Vector3 relativePos = closestEnemy.transform.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 35 * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.forward * 2f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //destroys missle if it hit the closest enemy
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
