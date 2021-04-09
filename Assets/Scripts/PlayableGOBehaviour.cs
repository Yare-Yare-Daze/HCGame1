using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableGOBehaviour : MonoBehaviour
{
    private bool weaponCollide = false;
    private bool playerCollide = false;
    private int health = 2;
    
    [SerializeField] private float speed;
    [SerializeField] private Transform planet;
    [SerializeField] private GameObject weapon;

    private float multiplier = 1;
    private Vector3 targetDirection = Vector3.zero;
    private Touch firstTouch;
    private Touch secondTouch;
    private float thirdScreenWidth;
    private int tapCount = 0;
    private float maxDoubleTouchTime = 0.15f;
    private float endDoubleTouchTime;
    private float ttime;

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public bool PlayerCollide
    {
        get { return playerCollide; }
        set { playerCollide = value; }
    }
    
    public bool WeaponCollide
    {
        get { return weaponCollide; }
        set { weaponCollide = value; }
    }
    
    private void Awake()
    {
        
    }

    void Start()
    {
        thirdScreenWidth = SceneBehaviour.screenWidth / 3.0f;
    }
    
    void Update()
    {
        targetDirection = Vector3.zero;
        
        weapon.SetActive(false);
        if (Input.touchCount > 0)
        {
            firstTouch = Input.GetTouch(0);

            checkTouchPosition(firstTouch);
            
            if (Input.touchCount > 1)
            {
                secondTouch = Input.GetTouch(1);
                checkTouchPosition(secondTouch);
            }

        }
        
        transform.RotateAround(planet.position, targetDirection, speed * multiplier * Time.deltaTime);
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

    private void checkTouchPosition(Touch touch)
    {
        if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
        {
            
            if (tapCount == 2 && ttime <= endDoubleTouchTime)
            {
                multiplier = 2;
            }
            else
            {
                multiplier = 1;
            }
            
            if (touch.position.x <= thirdScreenWidth)
            {
                targetDirection = Vector3.forward;
            }
            else if(touch.position.x >= thirdScreenWidth * 2)
            {
                targetDirection = Vector3.back;
            }
            else
            {
                weapon.SetActive(true);
            }
        }
        else if(Time.time > endDoubleTouchTime)
        {
            tapCount = 0;
            ttime = 0;
        }

        if (touch.phase == TouchPhase.Began)
        {
            tapCount += 1;
        }
        
        if (tapCount == 1)
        {
            endDoubleTouchTime = Time.time + maxDoubleTouchTime;
        }

        if (tapCount == 2 && ttime == 0)
        {
            ttime = Time.time;
        }
        
        Debug.Log(tapCount);
    }

}
