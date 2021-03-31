using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehaviour : MonoBehaviour
{
    public float speedRotate;
    public Vector3 rotateDirection;
    
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.Rotate(rotateDirection, speedRotate * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
