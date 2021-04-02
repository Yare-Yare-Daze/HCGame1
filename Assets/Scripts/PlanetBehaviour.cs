using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehaviour : MonoBehaviour
{
    [HideInInspector] public bool collide = false;
    
    public float speedRotate;
    public Vector3 rotateDirection;
    public GameObject armor;
    
    
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.Rotate(rotateDirection, speedRotate * Time.fixedDeltaTime);
        if (collide)
        {
            armor.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!collide) return;
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
    }
}
