using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{

    public Getinput input;
    public Livesui lives;
    public Wavescript waveScript;

    public float flySpeed;
    public float turnSpeed;
    public float strafeRotationSpeed;
    public float staticFlySpeed;
    public MeshRenderer mesh;
    private Transform spaceShip;
    public AudioSource audExplosion;
    public AudioSource audGetLife;
    private bool invincible;

    void Start()
    {
        //gets spaceship gameobject
        spaceShip = GetComponent<Transform>();
    }
    void FixedUpdate()
    {
        MoveY();
        MoveX();
        Rotate();
    }
    void MoveY()
    {
        //moves up and down
        float forwardSpeed = -input.buttonY * flySpeed + -staticFlySpeed;
        spaceShip.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }

    void MoveX()
    {
        //moves horizontal
        spaceShip.Translate(Vector3.right * -input.buttonX * flySpeed * Time.deltaTime);
    }

    void Rotate()
    {
        //rotates based on mousemovement
        spaceShip.Rotate(input.mouseY * turnSpeed * Time.deltaTime, input.mouseX * turnSpeed * Time.deltaTime, input.buttonX * strafeRotationSpeed * Time.deltaTime);
    }
    void OnCollisionEnter(Collision collision)
    {
        //checks if is being hit by enemy bullet
      if(collision.gameObject.CompareTag("EnemyBullet") && invincible == false)
        {
            //removes life and plays explosion audio
            Debug.Log("you have been hit");
            audExplosion.Play();
            lives.Damage();
            StartCoroutine(Damage_());
        }
      //checks if is hitting life
        if (collision.gameObject.CompareTag("Life"))
        {
            //adds life
            audGetLife.Play();
            lives.lives += 1;
            waveScript.score += 50;
        }
    }
    IEnumerator Damage_()
    {
        //makes flickering animation when being hit
        invincible = true;
        mesh.enabled = true;
        yield return new WaitForSeconds(0.1f);
        mesh.enabled = false;
        yield return new WaitForSeconds(0.1f);
        mesh.enabled = true;
        yield return new WaitForSeconds(0.1f);
        mesh.enabled = false;
        yield return new WaitForSeconds(0.1f);
        mesh.enabled = true;
        yield return new WaitForSeconds(0.1f);
        mesh.enabled = false;
        yield return new WaitForSeconds(0.1f);
        mesh.enabled = true;
        yield return new WaitForSeconds(0.5f);
        mesh.enabled = false;
        yield return new WaitForSeconds(0.1f);
        mesh.enabled = true;
        yield return new WaitForSeconds(1);
        invincible = false;
        yield return null;
    }
}
