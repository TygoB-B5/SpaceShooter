using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybullet : MonoBehaviour
{
    void FixedUpdate()
    {
        //moves bullet
        transform.Translate(Vector3.forward * 7f);
        Destroy(gameObject, 5);
    }
}
