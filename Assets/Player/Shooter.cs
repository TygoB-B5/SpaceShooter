using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    public Getinput input;
    public GameObject bullet;
    public GameObject missle;
    private Transform gun;
    private float t, c;
    private float t2 = 5;

    public AudioSource machineGunAud;
    public AudioSource missleAud;
    private int selectedGun;

    private int homingMissleAmount = 2;
    private int machineGunAmount = 100;

    private bool isReloading;
    private int waveTrigger;

    public GameObject machineGun;
    public GameObject homingMissle;
    public Text machineGunBullets;
    public Text homingMissleBullets;

    public Wavescript waveScript;

    void Start()
    {
        //sets default values and references
        waveTrigger = 1;
        isReloading = false;
        selectedGun = 1;

        machineGunBullets.text = machineGunAmount.ToString() + "/100";
        homingMissleBullets.text = homingMissleAmount.ToString() + "/5";

        gun = GetComponent<Transform>();
    }

    void Update()
    {
        //1 = machinegun, 2 = homingmissle
        SelectGuns();
        SelectGunsUI();
        Timers();
        Shooting();

        GiveHomingMissle();
    }

    void Timers()
    {
        //2 timer for the guns that count up every second
        t += 1 * Time.deltaTime;
        t2 += 1 * Time.deltaTime;
        c += 1 * Time.deltaTime;
    }

    void Shooting()
    {
        //checks if u are pressing fire1/are shooting)
        if (input.leftMouse && isReloading == false)
        {
            if (selectedGun == 1 && t > 0.07f && machineGunAmount > 0)
            {
                PewPew();
                t = 0;
                machineGunAmount -= 1;
                machineGunBullets.text = machineGunAmount.ToString() + "/100";
            }
            else if (selectedGun == 2 && t2 > 5 && homingMissleAmount > 0)
            {
                BigPew();
                t2 = 0;
                homingMissleAmount -= 1;
                homingMissleBullets.text = homingMissleAmount.ToString() + "/5";
            }
        }
        else if (input.R == true) //test if u reload
        {
            c = 0;
            StartCoroutine(Reload());
        }
    }

    void GiveHomingMissle()
    {
        //give 1 homingmissle per wave
        if (waveScript.waveNumber != waveTrigger)
        {
            homingMissleAmount += 1;
            homingMissleBullets.text = homingMissleAmount.ToString() + "/5";
            waveTrigger = waveScript.waveNumber;
        }
    }

    //machine gun shooting
    void PewPew()
    {
        Instantiate(bullet, gun.transform.position, gun.rotation);
        machineGunAud.Play();
    }
    //homing missle shooting
    void BigPew()
    {
        Instantiate(missle, gun.transform.position, gun.transform.rotation);
        missleAud.Play();
    }

    void SelectGuns()
    {
        //checks scrolwheel input and changes guns based on that
        if (input.scroll < 0)
        {
            selectedGun = 1;
        }
        else

        if (input.scroll > 0)
        {
            selectedGun = 2;
        }
    }
    void SelectGunsUI()
    {
        //changes sprite to machinegun- or misslesprite
        if (selectedGun == 1)
        {
            machineGun.SetActive(true);
        }
        else
        {
            machineGun.SetActive(false);
        }

        if (selectedGun == 2)
        {
            homingMissle.SetActive(true);
        }
        else
        {
            homingMissle.SetActive(false);
        }
    }

    //corountine for reloading 1 bullet every 0.05 seconds
    IEnumerator Reload()
    {
        isReloading = true;

        while (machineGunAmount < 100)
        {
            if (c > 0.01f)
            {
                machineGunAmount += 1;
                machineGunBullets.text = machineGunAmount.ToString() + "/100";
                c = 0;
            }
            yield return 0;
        }
        isReloading = false;

    }
}
