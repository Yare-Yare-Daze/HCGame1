using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SceneBehaviour : MonoBehaviour
{
    [HideInInspector] public static float camHight;
    [HideInInspector] public static float camWidth;
    [HideInInspector] public static float screenHight;
    [HideInInspector] public static float screenWidth;
    
    [SerializeField] private Vector2 force;
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text healthText;
    [SerializeField] private Text looseText;

    private int score = 0;
    private GameObject enemyGO;
    private Rigidbody2D rb2d;
    private PlayableGOBehaviour playableGOBehaviour;
    private PlanetBehaviour planetBehaviour;
    private Vector2 rateWidth;
    private Vector2 rateHight;

    private void Awake()
    {
        if(!camera) return;
        camHight = camera.orthographicSize;
        camWidth = camHight * camera.aspect;
        rateHight = new Vector2(-camHight, camHight);
        rateWidth = new Vector2(-camWidth, camWidth);

        screenHight = Screen.height;
        screenWidth = Screen.width;
    }
    void Start()
    {
        playableGOBehaviour = GameObject.Find("Player").GetComponent<PlayableGOBehaviour>();
        planetBehaviour = GameObject.Find("Planet").GetComponent<PlanetBehaviour>();
        scoreText.text = "Score: " + score;
        healthText.text = "Health: " + playableGOBehaviour.Health;
        looseText.gameObject.SetActive(false);
        StartCoroutine("spawnEnemy");
    }

    void Update()
    {
        if (playableGOBehaviour.WeaponCollide)
        {
            score++;
            scoreText.text = "Score: " + score;
            playableGOBehaviour.WeaponCollide = false;
        }

        if (playableGOBehaviour.PlayerCollide)
        {
            healthText.text = "Health: " + playableGOBehaviour.Health;
            playableGOBehaviour.PlayerCollide = false;
        }

        if (playableGOBehaviour.Health <= 0)
        {
            playableGOBehaviour.gameObject.SetActive(false);
            StartCoroutine(endGame());
        }

        if (!planetBehaviour.gameObject.activeSelf)
        {
            StartCoroutine(endGame());
        }
;    }

    private IEnumerator spawnEnemy()
    {
        while (true)
        {
            float xSpawnPos = Random.Range(rateWidth.x, rateWidth.y);
            float ySpawnPos = Random.Range(rateHight.x, rateHight.y);
            if (ySpawnPos >= 0)
            {
                ySpawnPos = rateHight.y;
                force = new Vector2(Random.Range(-20, 20), -100);
            }
            else
            {
                ySpawnPos = rateHight.x;
                force = new Vector2(Random.Range(-20, 20), 100);
            }
            Vector3 spawnPosition = new Vector3(xSpawnPos, ySpawnPos, 0);
            
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity).GetComponent<Rigidbody2D>().AddForce(force);
            yield return new WaitForSeconds(1.5f);
        }
    }

    public IEnumerator endGame()
    {
        Time.timeScale = 0;
        looseText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(2.0f);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        
    }
}
