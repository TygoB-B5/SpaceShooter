using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 60);
    }
    void Update()
    {
        transform.Rotate(0, 0, 150 * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, 0);
        }
    }
}
