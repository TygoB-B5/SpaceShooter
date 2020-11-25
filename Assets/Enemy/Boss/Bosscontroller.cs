using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosscontroller : MonoBehaviour
{
    private Transform player;
    private GameObject playerFinder;
    private Transform enemy;
    private Transform gun;
    public GameObject bullet;
    private float randomRotX, randomRotY, randomRotZ;
    private float t;
    private float t2;
    private float t3;
    private float distance;
    private Vector3 pos;
    private Quaternion lookPos;
    private bool isRotationToPlayer;
    public AudioSource aud;
    public AudioSource audExplosion;
    public ParticleSystem particle;
    public GameObject thisObject;
    private bool hitOnce;
    public int hp = 100;

    public Wavescript waveScript;
    public BossUI bossUI;

    void Start()
    {
        //makes references to needed objects
        bossUI = FindObjectOfType<BossUI>();
        bossUI.BossUI_();
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
        //moves and rotates boss
        enemy.Translate(Vector3.forward * 30 * Time.deltaTime);
        enemy.transform.Rotate(randomRotX * Time.deltaTime, randomRotY * Time.deltaTime, randomRotZ * Time.deltaTime);
        distance = Vector3.Distance(enemy.transform.position, player.transform.position);
        Timer();
        GetPlayerDirection();
        RotateToPlayer();
        RandomShoot();
    }

    void Timer()
    {
        //counting float for shooting
        t += 1 * Time.deltaTime;
        t2 = t2 + Random.Range(0.2f, 2) * Time.deltaTime;
        t3 += 1 * Time.deltaTime;
    }

    void RandomShoot()
    {
        //randomly shoots every few seconds
        if (t2 > 2)
        {
            Shoot();
            t2 = 0;
        }
    }

    void GetPlayerDirection()
    {
        //gets the position of the player
        pos = player.transform.position - enemy.transform.position;
        lookPos = Quaternion.LookRotation(pos);
    }

    void RotateToPlayer()
    {
        //rotates to player smoothly
        if (isRotationToPlayer == true)
        {
            enemy.rotation = Quaternion.RotateTowards(enemy.rotation, lookPos, 100 * Time.deltaTime);
            Shoot();
        }
        else
        {
            enemy.rotation = Quaternion.RotateTowards(enemy.rotation, lookPos, -10 * Time.deltaTime);
        }
    }

    IEnumerator RandomRotation()
    {
        //makes random decision based on random int values
        int ran = (Random.Range(1, 7));
        int ran2 = (Random.Range(1, 7));

        isRotationToPlayer = false;

        //checks if the distance between boss and player is higher than 200
        if (distance > 200)
        {
            ran = 5;
            ran2 = 5;
        }

        //checks if distance is higher than 350 to move back to the player
        if (distance > 350)
        {
            enemy.rotation = lookPos;
        }

        //checks if distance is lower than 75 to move away from the player
        if (distance < 50)
        {
            ran = Random.Range(1, 4);
            ran2 = Random.Range(1, 4);
        }


        //makes the decisions how to rotate based on random int variables
        switch (ran)
        {
            case 1:
                randomRotX = 0;
                randomRotY = 0;
                randomRotZ = 0;
                break;

            case 2:
                randomRotX = (Random.Range(-100, 100));
                randomRotY = (Random.Range(-10, 10));
                randomRotZ = (Random.Range(-10, 10));
                break;

            case 3:
                randomRotX = (Random.Range(-10, 10));
                randomRotY = (Random.Range(-100, 100));
                randomRotZ = (Random.Range(-10, 10));
                break;

            case 4:
                randomRotX = (Random.Range(-10, 10));
                randomRotY = (Random.Range(-10, 10));
                randomRotZ = (Random.Range(-100, 100));
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

        //same as first switch statement
        switch (ran2)
        {
            case 1:
                randomRotX = 0;
                randomRotY = 0;
                randomRotZ = 0;
                break;

            case 2:
                randomRotX = (Random.Range(-100, 100));
                randomRotY = (Random.Range(-10, 10));
                randomRotZ = (Random.Range(-10, 10));
                break;

            case 3:
                randomRotX = (Random.Range(-10, 10));
                randomRotY = (Random.Range(-100, 100));
                randomRotZ = (Random.Range(-10, 10));
                break;

            case 4:
                randomRotX = (Random.Range(-10, 10));
                randomRotY = (Random.Range(-10, 10));
                randomRotZ = (Random.Range(-100, 100));
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
        //shoots bullet
        if (t > 0.05f)
        {
            Instantiate(bullet, gun.transform.position, gun.rotation);
            aud.Play();
            t = 0;

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //checks if the player or bullet hit the boss
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Player"))
        {
            if (hitOnce == true)
            {
                Debug.Log("you hit the boss");
                audExplosion.Play();
                DamageBoss();
                hitOnce = false;
                StartCoroutine(HitReset());
            }
        }
    }
    IEnumerator HitReset()
    {
        //makes the boss invnicible for 1 sec
        yield return new WaitForSeconds(1);
        hitOnce = true;
        yield return 0;
    }

    void DamageBoss()
    {
        //if the boss it hit this changes the hp and if hp < 0 the boss dies
        hp -= 10;
        bossUI.BossUI_();
        if (hp <= 0)
        {
            
            audExplosion.Play();
            particle.Play();
            waveScript.BossKilled();
            Destroy(thisObject, 1.5f);
        }
    }
}
