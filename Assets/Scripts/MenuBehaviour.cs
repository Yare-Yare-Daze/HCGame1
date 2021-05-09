using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    private int _shipIndex = 0;
    private AudioSource _clickAudio;
    private List<GameObject> _shipsGOs = new List<GameObject>();

    [SerializeField] private List<GameObject> _menus;
    [SerializeField] private Transform _mainGOSpaceShip;
    
    void Start()
    {
        _shipIndex = PlayerPrefs.GetInt("shipIndex");
        _clickAudio = GetComponent<AudioSource>();
        for (int i = 0; i < _mainGOSpaceShip.childCount; i++)
        {
            _shipsGOs.Add(_mainGOSpaceShip.GetChild(i).gameObject);
            print(_shipsGOs[i]);
        }
    }

    void Update()
    {
        
    }

    public void clickPlayButton()
    {
        _clickAudio.Play();
        SceneManager.LoadScene("Play Scene");
    }

    public void clickShopButton()
    {
        _clickAudio.Play();
        _menus[0].SetActive(false);
        _menus[1].SetActive(true);
    }

    public void clickBackMenuButton()
    {
        _clickAudio.Play();
        _menus[1].SetActive(false);
        _menus[0].SetActive(true);
    }

    public void clickNextShipButton()
    {
        _clickAudio.Play();
        if (_shipIndex < _shipsGOs.Count - 1)
        {
            _shipsGOs[_shipIndex].SetActive(false);
            _shipIndex++;
            PlayerPrefs.SetInt("shipIndex", _shipIndex);
            _shipsGOs[_shipIndex].SetActive(true);
        }
        print(_shipIndex);
    }

    public void clickPreviousShipButton()
    {
        _clickAudio.Play();
        if (_shipIndex > 0)
        {
            _shipsGOs[_shipIndex].SetActive(false);
            _shipIndex--;
            PlayerPrefs.SetInt("shipIndex", _shipIndex);
            _shipsGOs[_shipIndex].SetActive(true);
        }
        print(_shipIndex);
    }
}
