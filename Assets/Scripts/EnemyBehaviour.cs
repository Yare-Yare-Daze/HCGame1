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
            other.GetComponentInParent<PlayableGOMovement>().WeaponCollide = true;
            Destroy(gameObject);
        }
        else if (other.tag == "Armor")
        {
            other.GetComponentInParent<PlanetBehaviour>().Collide = true;
            Destroy(gameObject);
        }
    }
}
