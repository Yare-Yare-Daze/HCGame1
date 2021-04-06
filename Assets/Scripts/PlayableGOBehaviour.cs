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
    private Touch firstTouch;
    private Touch secondTouch;
    float thirdScreenWidth;

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
        /*if (Input.GetKey(KeyCode.Space)) { multiplier = 2; }
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
        }*/

       
        weapon.SetActive(false);
        if (Input.touchCount > 0)
        {
            firstTouch = Input.GetTouch(0);
            if (Input.touchCount > 1)
            {
                secondTouch = Input.GetTouch(1);
            }

            checkTouchPosition(firstTouch);
            checkTouchPosition(secondTouch);

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

    private void checkTouchPosition(Touch touch)
    {            
        if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
        {
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
    }

}
