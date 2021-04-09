﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehaviour : MonoBehaviour
{
    private bool collide = false;
    
    [SerializeField] private float speedRotate;
    [SerializeField] private Vector3 rotateDirection;
    [SerializeField] private GameObject armor;

    public bool Collide
    {
        get { return collide; }
        set { collide = value; }
    }
    
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
