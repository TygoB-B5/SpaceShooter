using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameraswipe : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(-3 * Time.deltaTime, -1 * Time.deltaTime, 0);
    }
}
