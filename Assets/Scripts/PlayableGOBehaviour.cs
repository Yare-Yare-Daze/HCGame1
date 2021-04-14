using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableGOBehaviour : MonoBehaviour
{
    private bool weaponCollide = false;

    [SerializeField] private float speed;
    [SerializeField] private Transform planet;
    [SerializeField] private GameObject weapon;

    private float multiplier = 1;
    private Vector3 targetDirection = Vector3.zero;
    private Touch firstTouch;
    private Touch secondTouch;
    private float halfScreenWidth;
    private bool isRight = false;
    private int tapCount = 0;
    private float maxDoubleTouchTime = 0.2f;
    private float endDoubleTouchTime;
    private float timeTwoTapCount;
    private bool isDoubleTouch = false;

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
        halfScreenWidth = SceneBehaviour.screenWidth / 2.0f;
        weapon.SetActive(true);
    }
    
    void Update()
    {
        targetDirection = Vector3.zero;
        
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

    private void checkTouchPosition(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
        {
            tapCount += 1;
        }
        
        if (tapCount == 2 && timeTwoTapCount == 0)
        {
            Debug.Log("It is working!");
            timeTwoTapCount = Time.time;
        }
        
        if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
        {
            
            if (tapCount >= 2)
            {
                if ((timeTwoTapCount <= endDoubleTouchTime) && checkDirection(touch))
                {
                    multiplier = 2;
                }
                isDoubleTouch = true;
            }
            else
            {
                multiplier = 1;
            }
            
            if (touch.position.x <= halfScreenWidth)
            {
                targetDirection = Vector3.forward;
                
            }
            else
            {
                targetDirection = Vector3.back;
                
            }
        } 
        else if(Time.time > endDoubleTouchTime && isDoubleTouch)
        {
            tapCount = 0;
            timeTwoTapCount = 0;
            isDoubleTouch = false;
        }
        
        if (touch.phase == TouchPhase.Ended)
        {
            if (tapCount == 1)
            {
                endDoubleTouchTime = Time.time + maxDoubleTouchTime;
            }

            if (touch.position.x <= halfScreenWidth)
            {
                isRight = false;
            }
            else
            {
                isRight = true;
            }
        }
        

        Debug.Log(tapCount);
        Debug.Log(endDoubleTouchTime);
        Debug.Log(checkDirection(touch));
    }

    private bool checkDirection(Touch touch)
    {
        if ((isRight && touch.position.x > halfScreenWidth) || (!isRight && touch.position.x <= halfScreenWidth))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
