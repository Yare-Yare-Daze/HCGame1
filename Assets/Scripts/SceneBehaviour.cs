using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SceneBehaviour : MonoBehaviour
{
    [HideInInspector]public static float camHight;
    [HideInInspector]public static float camWidth;
    
    public Vector2 force;
    public Camera camera;
    public GameObject enemyPrefab;

    private GameObject enemyGO;
    private Rigidbody2D rb2d;
    private Vector2 rateWidth;
    private Vector2 rateHight;

    private void Awake()
    {
        if(!camera) return;
        camHight = camera.orthographicSize;
        camWidth = camHight * camera.aspect;
        rateHight = new Vector2(-camHight, camHight);
        rateWidth = new Vector2(-camWidth, camWidth);
        //Debug.Log(camHight);
        //Debug.Log(camWidth);
    }

    void Start()
    {
        StartCoroutine("spawnEnemy");
    }

    void Update()
    {
        
    }

    private IEnumerator spawnEnemy()
    {
        while (true)
        {
            float xSpawnPos = Random.Range(rateWidth.x, rateWidth.y);
            float ySpawnPos = Random.Range(rateHight.x, rateHight.y);
            if (ySpawnPos >= 0)
            {
                ySpawnPos = rateHight.y;
                force = new Vector2(Random.Range(-1, 1), -100);
            }
            else
            {
                ySpawnPos = rateHight.x;
                force = new Vector2(Random.Range(-1, 1), 100);
            }
            Vector3 spawnPosition = new Vector3(xSpawnPos, ySpawnPos, 0);
            //REWORK
            enemyGO = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            rb2d = enemyGO.GetComponent<Rigidbody2D>();
            rb2d.AddForce(force);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
