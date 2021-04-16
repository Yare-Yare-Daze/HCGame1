using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableGOMovement : MonoBehaviour
{
    private bool weaponCollide = false;

    [SerializeField] private float speed;
    [SerializeField] private Transform planet;
    [SerializeField] private GameObject weapon;

    private float multiplier = 1;
    private Vector3 targetDirection = Vector3.zero;

    public bool WeaponCollide
    {
        get { return weaponCollide; }
        set { weaponCollide = value; }
    }

    public float Miltiplier
    {
        get { return multiplier; }
        set { multiplier = value; }
    }

    public Vector3 TargetDirection
    {
        get { return targetDirection; }
        set { targetDirection = value; }
    }
    
    private void Awake()
    {
        
    }

    void Start()
    {
        weapon.SetActive(true);
    }
    
    void Update()
    {
        transform.RotateAround(planet.position, targetDirection, speed * multiplier * Time.deltaTime);
    }
    
}
