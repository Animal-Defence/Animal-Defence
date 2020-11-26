﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SimpleMonster : MonoBehaviour
{

    public float speed; // 스피드
    public int health; // 체력
    public int enemy_point;
    public Sprite[] sprites; //평소이미지, 데미지를 입었을때의 이미지
    public Text enemeyHealth;
    SpriteRenderer spriteRenderer; 
    Rigidbody2D rigid; // 속력 조절
    AnimalAdd animalAdd;
    PlayerHP playerHP;
    //public string coin_string;

    private void Awake() //초기화
    {
        //if (GameObject.Find("Boss_Enemy").GetComponent<BossMonster>())
        //    Set_BossEnemeyHealth();
        //else 
        if (gameObject.tag == "Boss")
        {
            Set_BossEnemeyHealth();
        }
        else
        {
            Set_enemeyHealth();
        }
        enemeyHealth.text = "HP" + health;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.down * speed; // 속력 조절. 2D이기대문에 Vector2
    }

    void Start()
    {
        animalAdd = GameObject.Find("AnimalAdd").GetComponent<AnimalAdd>();
        playerHP = GameObject.Find("PlayerHP").GetComponent<PlayerHP>();
    }

    void onHit(int dmg)
    {
        health -= dmg;
        //spriteRenderer.sprite = sprites[1]; // 인스펙터창에서 데미지 입었을 경우 이미지가 변하도록 설정
        //Invoke("returnSprite", 0.1f); // 함수 이름을 문자열로. 시간차를 줄땐 Invoke

        enemeyHealth.text = "HP" + health;

        if (health <= 0)
        {
            Player_Coin player_Coin = GameObject.Find("Player_Coin").GetComponent<Player_Coin>();
            Player_Coin.coin_score += enemy_point;
            player_Coin.coin_text_obj.text = "" + Player_Coin.coin_score;
            
            Destroy(gameObject);
            if (gameObject.tag == "Enemy")
                EnemyDeathManager.killEnemyNum++;
            else if (gameObject.tag == "Boss")
                EnemyDeathManager.killBossNum++;

        }
    }

    void returnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Floor":
                playerHP.MinusHP();
                Destroy(gameObject);
                break;
            case "GamerBullet":
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                //현지의 스크립트에서 총알의 데미지를 가져옴.
                //int dmg = 가져온 총알의 데미지.
                onHit(bullet.dmg);
                Destroy(collision.gameObject);
                break;
            case "Ground1":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                animalAdd.falseGround(0);
                break;
            case "Ground2":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                animalAdd.falseGround(1);
                break;
            case "Ground3":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                animalAdd.falseGround(2);
                break;
            case "Ground4":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                animalAdd.falseGround(3);
                break;
            case "Ground5":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                animalAdd.falseGround(4);
                break;
            case "Ground6":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                animalAdd.falseGround(5);
                break;
            case "Ground7":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                animalAdd.falseGround(6);
                break;
            case "Ground8":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                animalAdd.falseGround(7);
                break;
            case "Ground9":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                animalAdd.falseGround(8);
                break;
            case "Ground10":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                animalAdd.falseGround(9);
                break;
            default:
                break;
        }
        /*
        if (collision.gameObject.tag == "Floor"){
            Destroy(gameObject);//삭제하는 함수
            //플레이어의 HP 깎는다
        }
        else if (collision.gameObject.tag == "GamerBullet") {// 동물들의 공격을 맞을 경우
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            //현지의 스크립트에서 총알의 데미지를 가져옴.
            //int dmg = 가져온 총알의 데미지.
            onHit(bullet.dmg);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            for (int i = 0; i < 10; i++)
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
                string groundName = string.Format("ground{0}", i+1);
                if (GameObject.Find(groundName) == null)
                {
                    Debug.Log(groundName);
                    AnimalAdd animalAdd = GameObject.Find("AnimalAdd").GetComponent<AnimalAdd>();
                    animalAdd.falseGround(i);
                }
            }
        }
        else if (collision.gameObject.tag == "Animal")
        {
            //동물의 단계를 낮춘다.
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        */

    }

    public void Set_enemeyHealth()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        health += gameManager.Enemy_HP * 2;
    }

    public void Set_BossEnemeyHealth()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        health += gameManager.Enemy_HP * 10;
    }

    
    public void Set_Enemy_Speed_Plus()
    {
        //속도높임
    }

    public void Set_Enemy_Nomal()
    {
        //속도원래대로
    }


    // Start is called before the first frame update


    /*
    //중력 사용할때는 2D붙인다.
    //충돌한 순간
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Floor")
        {
            Debug.Log("여기서 자신을 삭제하는 코드.");
            //Destroy(collision.gameObject);//닿은 물체 삭제
            Destroy(this.gameObject,1);//1초뒤 자신삭제(floor를 부수고 사라지는 모션을 넣을예정)
            //충돌 이벤트 처리
        }

        //동물에게 닿을경우
        //1. 동물에게 데미지를 준다
        //2. 자신은 삭제된다.
        //3. 마지막 동물을 죽였을 경우, 해당 동물의 floor는 더이상 사용할수 없다.


    }
    */


    /*
     *OnCollstionStay // 충돌하는 순간 
     * OnCollisionExit // 충돌했다가 분리되는 순간
     *
     */



}
