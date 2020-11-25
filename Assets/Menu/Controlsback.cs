using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Controlsback : MonoBehaviour
{
    public void back()
    {
        SceneManager.LoadScene("Menu");
        Debug.Log("back");
    }
}
