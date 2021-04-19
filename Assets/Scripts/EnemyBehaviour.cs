using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject _explosionPrefab;
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
            Explosion();
        }
        else if (other.tag == "Armor")
        {
            other.GetComponentInParent<PlanetBehaviour>().Collide = true;
            Explosion();
        }
    }

    private void Explosion()
    {
        Instantiate(_explosionPrefab).transform.position = gameObject.transform.position;
        
        Destroy(gameObject);
    }
}
