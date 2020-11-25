using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Livesui : MonoBehaviour
{
    public float lives;

    public Text whiteText;
    public Text redText;

    void Start()
    {
        //disables red text from start to make sure the red and white texts don't overlap
        redText.enabled = false;
    }

    void Update()
    {
        //updates lives text
        whiteText.text = "Lives:" + lives.ToString();
        redText.text = "Lives:" + lives.ToString();
    }

    public void Damage()
    {
        //changes lives and calls gameover function if the lives are -1
        lives -= 1;
        if (lives < 0)
        {
            GameOver();
        }
        StartCoroutine("RedWhiteSwitch");
    }

    IEnumerator RedWhiteSwitch()
    {
        //animates red and white flickering of text when damage is taken
        redText.enabled = false;
        whiteText.enabled = true;
        yield return new WaitForSeconds(0.5f);
        redText.enabled = true;
        whiteText.enabled = false;
        yield return new WaitForSeconds(0.5f);
        redText.enabled = false;
        whiteText.enabled = true;
        yield return new WaitForSeconds(0.5f);
        redText.enabled = true;
        whiteText.enabled = false;
        yield return new WaitForSeconds(0.5f);
        redText.enabled = false;
        whiteText.enabled = true;
        yield return 0;
    }

    void GameOver()
    {
        //unlocks cursor and changes scene when game over
        Screen.lockCursor = false;
        SceneManager.LoadScene("Gameover");
    }
}
