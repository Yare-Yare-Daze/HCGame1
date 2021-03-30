using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehaviour : MonoBehaviour
{
    public float speedRotate;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward, speedRotate * Time.fixedDeltaTime);
    }
}
