using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletmovement : MonoBehaviour
{
    
    void FixedUpdate()
    {
        //moves bullet
        transform.Translate(Vector3.back * 7.5f);
        Destroy(gameObject, 5);
    }
}
