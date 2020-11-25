using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    public Text bossText;
    public Bosscontroller bossController;
    private int hp;
    void Start()
    {
        //disables boss text and sets begin text
        bossText.enabled = false;
        bossText.text = "Boss hp: 100" + hp.ToString();
    }

    public void BossUI_()
    {
        //finds boss gameobject and chnages text to the boss hp
        bossText.enabled = true;
        bossController = FindObjectOfType<Bosscontroller>();
        hp = bossController.hp;
        bossText.text = "Boss hp: " + hp.ToString();
        if(hp <= 0)
        {
            bossText.enabled = false;
        }
    }
}
