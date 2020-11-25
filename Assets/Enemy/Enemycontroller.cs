 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemycontroller : MonoBehaviour
{
    private Transform player;
    private GameObject playerFinder;
    private Transform enemy;
    private Transform gun;
    public GameObject bullet;
    private float randomRotX, randomRotY, randomRotZ;
    private float t;
    private float t2;
    private float distance;
    private Vector3 pos;
    private Quaternion lookPos;
    private bool isRotationToPlayer;
    public AudioSource aud;
    public AudioSource audExplosion;
    public ParticleSystem particle;
    public GameObject thisObject;
    private bool hitOnce;

    public Wavescript waveScript;

    void Start()
    {
        //makes references to needed objects
        gun = GetComponentInChildren<Transform>();
        playerFinder = GameObject.FindGameObjectWithTag("Player");
        waveScript = FindObjectOfType<Wavescript>(); 
        player = playerFinder.GetComponent<Transform>();
        enemy = GetComponent<Transform>();
        StartCoroutine(RandomRotation());
        hitOnce = true;
    }

    void Update()
    {
        //moves and rotates the enemy
        enemy.Translate(Vector3.forward * 12 * Time.deltaTime);
        enemy.transform.Rotate(randomRotX * Time.deltaTime, randomRotY * Time.deltaTime, randomRotZ * Time.deltaTime);
        distance = Vector3.Distance(enemy.transform.position, player.transform.position);
        t += 1 * Time.deltaTime;
        GetPlayerDirection();
        RotateToPlayer();
        RandomShoot();
    }

    void RandomShoot()
    {
        //shoots randomly
        t2 = t2 + Random.Range(0.2f, 2) * Time.deltaTime;
        if (t2 > 2)
        {
            Shoot();
            t2 = 0;
        }
    }

    void GetPlayerDirection()
    {
        //gets the lookposition to look at the player
        pos = player.transform.position - enemy.transform.position;
        lookPos = Quaternion.LookRotation(pos);
    }

    void RotateToPlayer()
    {
        //rotates to player smoothly
        if (isRotationToPlayer == true)
        {
            enemy.rotation = Quaternion.RotateTowards(enemy.rotation, lookPos, 50 * Time.deltaTime);
            Shoot();
        }
        else
        {
            enemy.rotation = Quaternion.RotateTowards(enemy.rotation, lookPos, 10 * Time.deltaTime);
        }
    }

    IEnumerator RandomRotation()
    {
        //random int values for decisions
        int ran = (Random.Range(1, 7));
        int ran2 = (Random.Range(1, 7));
        isRotationToPlayer = false;

        //rotates enemy back smoothly to player if too far
        if (distance > 200)
        {
            ran = 5;
            ran2 = 5;
        }

        //rotates enemy back to player if too far
        if (distance > 350)
        {
            enemy.rotation = lookPos;
        }

        //rotates based on random int values or locks to player
        switch (ran)
        {
            case 1:
                randomRotX = 0;
                randomRotY = 0;
                randomRotZ = 0;
                break;

            case 2:
                randomRotX = (Random.Range(-50, 50));
                randomRotY = (Random.Range(-10, 10));
                randomRotZ = (Random.Range(-10, 10));
                break;

            case 3:
                randomRotX = (Random.Range(-10, 10));
                randomRotY = (Random.Range(-50, 50));
                randomRotZ = (Random.Range(-10, 10));
                break;

            case 4:
                randomRotX = (Random.Range(-10, 10));
                randomRotY = (Random.Range(-10, 10));
                randomRotZ = (Random.Range(-50, 50));
                break;

            case 5:
                isRotationToPlayer = true;
                break;

            case 6:
                isRotationToPlayer = true;
                break;


            case 7:
                isRotationToPlayer = true;
                break;
        }

        //if the distance is too low the enemy will rotate away
        if (distance < 20)
        {
            randomRotX = (Random.Range(-200, 200));
            randomRotY = (Random.Range(-200, 200));
            randomRotZ = (Random.Range(-200, 200));
        }

        //rotates based on random int values or locks to player (the same)
        switch (ran2)
        {
            case 1:
                randomRotX = 0;
                randomRotY = 0;
                randomRotZ = 0;
                break;

            case 2:
                randomRotX = (Random.Range(-50, 50));
                randomRotY = (Random.Range(-10, 10));
                randomRotZ = (Random.Range(-10, 10));
                break;

            case 3:
                randomRotX = (Random.Range(-10, 10));
                randomRotY = (Random.Range(-50, 50));
                randomRotZ = (Random.Range(-10, 10));
                break;

            case 4:
                randomRotX = (Random.Range(-10, 10));
                randomRotY = (Random.Range(-10, 10));
                randomRotZ = (Random.Range(-50, 50));
                break;

            case 5:
                isRotationToPlayer = true;
                break;

            case 6:
                isRotationToPlayer = true;
                break;

            case 7:
                isRotationToPlayer = true;
                break;
        }

                yield return new WaitForSeconds(Random.Range(0.1f, 3f));

        yield return StartCoroutine(RandomRotation());
    }

    void Shoot()
    {
        //shoots the bullet
        if (t > 0.25f)
        {
            Instantiate(bullet, gun.transform.position, gun.rotation);
            aud.Play();
            t = 0;
            
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //checks if the enemy is touching a bullet or the player
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Player"))
        {
            if (hitOnce == true)
            {
                //kills the enemy
                Debug.Log("you hit the enemy");
                audExplosion.Play();
                particle.Play();
                waveScript.Killed();
                Destroy(thisObject, 1.5f);
                hitOnce = false;
            }


        }
    }
}
