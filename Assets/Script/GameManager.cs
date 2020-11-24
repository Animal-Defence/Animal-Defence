﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public GameObject bossObj;

    public float maxSpqwnDelay;
    public float curSpawnDelay; // 현재 흐르고 있는 시간
    public float bossSpawnDelay;
    //public float playTime;

    public int playerPoint;

    public int Enemy_HP;

    //public Text Coin_score;//Coin의 점수

    private void Awake()
    {
        Enemy_HP = 0;
        curSpawnDelay = 0;
    }

    private void Update()
    {
        curSpawnDelay += Time.deltaTime;
        bossSpawnDelay += Time.deltaTime;
        //playTime += Time.deltaTime;

        //적 스폰 시간
        if (curSpawnDelay > maxSpqwnDelay)
        {
            spawnEnemy();
            maxSpqwnDelay = Random.Range(0.5f, 3f); // 랜덤시간으로 생성.

            curSpawnDelay = 0;
        }

        //보스 스폰 시간
        if(bossSpawnDelay > 7f)
        {
            spawnBoss();
            bossSpawnDelay = 0;
        }

    }


    void spawnBoss()
    {
        //getvalue
        int ranPoint = Random.Range(0, spawnPoints.Length);

        Instantiate(bossObj,
            spawnPoints[ranPoint].position,
            spawnPoints[ranPoint].rotation);
    }

    public void spawnEnemy()
    {
        int ranEnemey = Random.Range(0, enemyObjs.Length); // 지금 적이 보스와 일반 몬스터 뿐
        int ranPoint = Random.Range(0, spawnPoints.Length);

        Instantiate(enemyObjs[ranEnemey], 
            spawnPoints[ranPoint].position, 
            spawnPoints[ranPoint].rotation);

        Enemy_HP += 1;

    }
}

