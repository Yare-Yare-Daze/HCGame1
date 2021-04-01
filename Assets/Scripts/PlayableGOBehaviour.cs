using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableGOBehaviour : MonoBehaviour
{
    public float speed;
    public Transform planet;
    public Camera camera;

    private bool isRight = true;
    private float multiplier = 1;
    private Vector3 targetDirection = Vector3.zero;

    private void Awake()
    {
        //camHight = camera.orthographicSize;
        //camWidth = camHight * camera.aspect;
        //Debug.Log(camWidth);
        //Debug.Log(camHight);
    }

    void Start()
    {
        //Debug.Log(screenWidth);
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) { multiplier = 2; }
        else { multiplier = 1; }
        targetDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            targetDirection = Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            targetDirection = Vector3.back;
        }
        
        transform.RotateAround(planet.position, targetDirection, speed * multiplier * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
