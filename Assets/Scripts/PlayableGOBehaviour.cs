using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableGOBehaviour : MonoBehaviour
{
    public float speed;
    public Vector2 rangePositions;

    private bool isRight = true;
    private Vector3 targetDirection = Vector3.right;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (isRight && transform.position.x >= rangePositions.y)
        {
            targetDirection = Vector3.left;
            isRight = false;
        }
        else if (!isRight && transform.position.x <= rangePositions.x)
        {
            targetDirection = Vector3.right;
            isRight = true;
        }
        
        transform.position += targetDirection * (speed * Time.deltaTime);
    }
    
}
