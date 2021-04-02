using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableGOBehaviour : MonoBehaviour
{
    [HideInInspector] public bool weaponCollide = false;
    [HideInInspector] public bool playerCollide = false;
    [HideInInspector] public int health = 2;
    
    public float speed;
    public Transform planet;
    public GameObject weapon;

    private bool isRight = true;
    private float multiplier = 1;
    private Vector3 targetDirection = Vector3.zero;

    private void Awake()
    {
        
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) { multiplier = 2; }
        else { multiplier = 1; }
        targetDirection = Vector3.zero;

        
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            weapon.SetActive(true);
        }
        else
        {
            weapon.SetActive(false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            targetDirection = Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            targetDirection = Vector3.back;
        }
        
        transform.RotateAround(planet.position, targetDirection, speed * multiplier * Time.deltaTime);

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            health--;
            playerCollide = true;
            Destroy(other.gameObject);
        }
    }
}
