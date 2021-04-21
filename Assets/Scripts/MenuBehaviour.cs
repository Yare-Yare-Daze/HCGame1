using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    private AudioSource _clickAudio;
    void Start()
    {
        _clickAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void PlayButton()
    {
        _clickAudio.Play();
        SceneManager.LoadScene(1);
    }
}
