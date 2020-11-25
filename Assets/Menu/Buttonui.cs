using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Buttonui : MonoBehaviour
{
    public void start()
    {
        SceneManager.LoadScene("Game");
    }

    public void controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void exit()
    {
        Application.Quit();
        Debug.Log("quit");
    }

    public void menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
