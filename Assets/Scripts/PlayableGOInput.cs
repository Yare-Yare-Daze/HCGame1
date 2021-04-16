using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableGOInput : MonoBehaviour
{
    private PlayableGOMovement _playableGOMovement;
    private Touch _fisrtTouch;
    private float _halfWidthScreen;
    private float _timeLastTouch1;
    private float _timeLastTouch2;
    private int _tapCount = 0;
    private bool _clockwise = false;

    [SerializeField] private float _timeRecognitionDoubleTouch = 0.2f;
    void Start()
    {
        _playableGOMovement = GetComponent<PlayableGOMovement>();
        _halfWidthScreen = SceneBehaviour.screenWidth / 2.0f;
    }
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            _fisrtTouch = Input.GetTouch(0);
            _playableGOMovement.TargetDirection = Vector3.zero;

            if (_fisrtTouch.phase == TouchPhase.Began)
            {
                _tapCount++;
                if (_tapCount == 2)
                {
                    _timeLastTouch2 = Time.time;
                }
            }
            
            if (_fisrtTouch.phase == TouchPhase.Stationary || _fisrtTouch.phase == TouchPhase.Moved)
            {
                if (checkTouchPositionXMoreThenHalfWidthScreen())
                {
                    _playableGOMovement.TargetDirection = Vector3.back;
                }
                else
                {
                    _playableGOMovement.TargetDirection = Vector3.forward;
                }

                if (_tapCount == 2 && _timeLastTouch2 <= (_timeLastTouch1 + _timeRecognitionDoubleTouch))
                {
                    if (_clockwise && checkTouchPositionXMoreThenHalfWidthScreen() ||
                        (!_clockwise && !checkTouchPositionXMoreThenHalfWidthScreen()))
                    {
                        _playableGOMovement.Miltiplier = 2;
                    }
                    
                }
                else
                {
                    _playableGOMovement.Miltiplier = 1;
                }
                
            }
            
            if (_fisrtTouch.phase == TouchPhase.Ended)
            {
                if (_tapCount == 1)
                {
                    _timeLastTouch1 = Time.time;
                    if (checkTouchPositionXMoreThenHalfWidthScreen())
                    {
                        _clockwise = true;
                    }
                    else
                    {
                        _clockwise = false;
                    }
                }
                
                
            }
        }
        else if (Time.time > (_timeLastTouch1 + _timeRecognitionDoubleTouch))
        {
            _tapCount = 0;
        }
        
        print(_tapCount);
        print(_clockwise);
    }

    private bool checkTouchPositionXMoreThenHalfWidthScreen()
    {
        return _fisrtTouch.position.x >= _halfWidthScreen;
    }
}
