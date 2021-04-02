using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Weapon")
        {
            other.GetComponentInParent<PlayableGOBehaviour>().weaponCollide = true;
            Destroy(gameObject);
        }
        else if (other.tag == "Armor")
        {
            other.GetComponentInParent<PlanetBehaviour>().collide = true;
            Destroy(gameObject);
        }
    }
}
