using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SceneBehaviour : MonoBehaviour
{
    [HideInInspector]public static float camHight;
    [HideInInspector]public static float camWidth;
    
    public Vector2 force;
    public Camera camera;
    public GameObject enemyPrefab;
    public Text scoreText;
    public Text healthText;

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
    }

    void Start()
    {
        playableGOBehaviour = GameObject.Find("Player").GetComponent<PlayableGOBehaviour>();
        planetBehaviour = GameObject.Find("Planet").GetComponent<PlanetBehaviour>();
        scoreText.text = "Score: " + score;
        healthText.text = "Health: " + playableGOBehaviour.health;
        StartCoroutine("spawnEnemy");
    }

    void Update()
    {
        if (playableGOBehaviour.weaponCollide)
        {
            score++;
            scoreText.text = "Score: " + score;
            playableGOBehaviour.weaponCollide = false;
        }

        if (playableGOBehaviour.playerCollide)
        {
            healthText.text = "Health: " + playableGOBehaviour.health;
            playableGOBehaviour.playerCollide = false;
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
            //rb2d = enemyGO.GetComponent<Rigidbody2D>();
            //rb2d.AddForce(force);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
