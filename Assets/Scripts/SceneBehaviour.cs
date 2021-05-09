using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SceneBehaviour : MonoBehaviour
{
    [HideInInspector] public static float _camHight;
    [HideInInspector] public static float _camWidth;
    [HideInInspector] public static float _screenHight;
    [HideInInspector] public static float _screenWidth;

    [SerializeField] private GameObject _playerGO;
    [SerializeField] private GameObject _planetGO;
    [SerializeField] private Vector2 _force;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _looseText;
    [SerializeField] private Text _helpText;
    [SerializeField] private bool _testMode = false;

    private int _score = 0;
    private int _tapHelpButton = 0;
    private int _shipIndex;
    private List<GameObject> _shipsGOs = new List<GameObject>();
    private GameObject _enemyGO;
    private Rigidbody2D _rb2d;
    private PlayableGOMovement _playableGOMovement;
    private PlanetBehaviour _planetBehaviour;
    private Vector2 _rateWidth;
    private Vector2 _rateHight;
    private Vector2 _torque = new Vector2(-0.3f,0.3f);
    private AudioSource _audioScore;

    private void Awake()
    {
        if(!_camera) return;
        _camHight = _camera.orthographicSize;
        _camWidth = _camHight * _camera.aspect;
        _rateHight = new Vector2(-_camHight, _camHight);
        _rateWidth = new Vector2(-_camWidth, _camWidth);

        _screenHight = Screen.height;
        _screenWidth = Screen.width;
    }
    void Start()
    {
        _playableGOMovement = _playerGO.GetComponent<PlayableGOMovement>();
        _planetBehaviour = _planetGO.GetComponent<PlanetBehaviour>();
        _scoreText.text = "Score: " + _score;
        _looseText.gameObject.SetActive(false);
        _audioScore = gameObject.GetComponentInChildren<AudioSource>();
        _shipIndex = PlayerPrefs.GetInt("shipIndex");
        Transform rootSpaceShipsGO = _playerGO.transform.GetChild(0);
        
        for (int i = 0; i < rootSpaceShipsGO.transform.childCount; i++)
        {
            _shipsGOs.Add(rootSpaceShipsGO.GetChild(i).gameObject);
            if (i == PlayerPrefs.GetInt("shipIndex"))
            {
                _shipsGOs[i].SetActive(true);
            }
            else
            {
                _shipsGOs[i].SetActive(false);
            }
        }
        
        if (!_testMode) { StartCoroutine("spawnEnemy"); }
    }

    void Update()
    {
        if (_playableGOMovement.WeaponCollide)
        {
            _score++;
            _audioScore.Play();
            if (PlayerPrefs.GetInt("ScoreInt") < _score)
            {
                PlayerPrefs.SetInt("ScoreInt", _score);
            }
            _scoreText.text = "Score: " + _score;
            _playableGOMovement.WeaponCollide = false;
        }

        if (!_planetBehaviour.gameObject.activeSelf)
        {
            StartCoroutine(endGame());
        }
        
    }

    private IEnumerator spawnEnemy()
    {
        while (true)
        {
            float xSpawnPos = Random.Range(_rateWidth.x, _rateWidth.y);
            float ySpawnPos = Random.Range(_rateHight.x, _rateHight.y);
            if (ySpawnPos >= 0)
            {
                ySpawnPos = _rateHight.y;
                _force = new Vector2(Random.Range(-20, 20), -100);
            }
            else
            {
                ySpawnPos = _rateHight.x;
                _force = new Vector2(Random.Range(-20, 20), 100);
            }
            Vector3 spawnPosition = new Vector3(xSpawnPos, ySpawnPos, 0);

            Rigidbody2D enemyRB2D = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity).GetComponent<Rigidbody2D>();
            enemyRB2D.AddForce(_force);
            enemyRB2D.AddTorque(Random.Range(_torque.x, _torque.y));
            
            yield return new WaitForSeconds(1.2f);
        }
    }

    public IEnumerator endGame()
    {
        Time.timeScale = 0;
        _looseText.text = "You loose \n Best score is " + PlayerPrefs.GetInt("ScoreInt", 0);
        _looseText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(2.0f);
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        
    }

    public void clickHelpButton()
    {
        _tapHelpButton++;

        if (_tapHelpButton == 1)
        {
            Time.timeScale = 0;
            _helpText.gameObject.SetActive(true);
        }
        else if (_tapHelpButton == 2)
        {
            _helpText.gameObject.SetActive(false);
            Time.timeScale = 1;
            _tapHelpButton = 0;
        }
    }
    
}
