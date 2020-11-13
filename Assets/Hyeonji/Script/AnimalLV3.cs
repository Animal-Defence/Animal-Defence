﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalLV3 : MonoBehaviour
{
    public Transform[] Animals4; // 4단계 동물 프리펩
    public GameObject Weaponpf; // 무기 프리펩
    private Rigidbody2D rigid;
    private int isDestroy = 0; // 충돌 횟수 확인
    public float maxShotDelay; // 총알 쏘는 시간
    public float curShotDelay;
    private Vector3 firstPosition; // 오브젝트 처음 위치 저장
    private Vector3 nowPosition; // 업그레이드 된 동물 생성될 위치
    private Vector3[] Positions = new Vector3[2]; // 충돌체 위치 저장
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        // 오브젝트 처음 위치 저장
        firstPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Raycast 확인
        Debug.DrawRay(rigid.position, Vector3.down, new Color(1, 0, 0));
        Attack();
        ShotDelay();
    }

    // 무기로 공격
    private void Attack()
    {
        float minLen = 3000.0f;
        int minIndex = 0;
        if (curShotDelay < maxShotDelay)
            return;

        GameObject weapon = Instantiate(Weaponpf, firstPosition, transform.rotation); // 무기 생성
        Rigidbody2D rigid = weapon.GetComponent<Rigidbody2D>();

        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy"); // Enemy Tag로 적들 찾기
        if (enemys.Length != 0)
        {
            for (int i = 0; i < enemys.Length; i++)
            {
                if (minLen >= (enemys[i].transform.position - gameObject.transform.position).sqrMagnitude)
                {
                    minLen = (enemys[i].transform.position - gameObject.transform.position).sqrMagnitude;
                    minIndex = i;
                }
            }
            Vector2 attackPos = enemys[minIndex].transform.position; // 가장 가까운 적의 x좌표
            attackPos.x = attackPos.x - gameObject.transform.position.x; // 자기 자신 x좌표 - 적의 x좌표
            rigid.AddForce(attackPos, ForceMode2D.Impulse); // 가까운 적을 향해 공격
        }
        else
        {
            rigid.AddForce(Vector2.up * 15, ForceMode2D.Impulse); // 적이없으면 그냥 위로 공격
        }

        curShotDelay = 0;
    }

    private void ShotDelay()
    {
        curShotDelay += Time.deltaTime;
    }
    // 마우스 클릭
    private void OnMouseDown()
    {
        // 충돌 오브젝트 0개 초기화
        isDestroy = 0;
    }

    // 마우스 드래그
    private void OnMouseDrag()
    {
        // 마우스 드래그하는 동안 위치를 오브젝트에 넣어줌, 물체가 포인터에 따라옴
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        this.transform.position = mousePosition;
    }

    // 마우스를 놓을 때
    private void OnMouseUp()
    {
        // RaycastAll 아래로 확인
        RaycastHit2D[] rayDown = Physics2D.RaycastAll(rigid.position, Vector3.down, 1);

        for (int i = 0; i < rayDown.Length; i++)
        {
            // 충돌한 동물 태그가 지금 동물 태그와 같다면 횟수+
            if (rayDown[i].collider.gameObject.tag == this.transform.tag)
            {
                isDestroy++;// 횟수가 2, 즉 본인과 충돌한 동물이 같다면
                if (isDestroy == 2)
                {
                    // 생성 될 위치 저장
                    Positions[0] = rayDown[i].collider.gameObject.transform.position;
                    Positions[1] = rayDown[i - 1].collider.gameObject.transform.position;
                    // 둘 다 삭제
                    Destroy(rayDown[i].collider.gameObject);
                    Destroy(rayDown[i - 1].collider.gameObject);
                    // 생성 될 위치 확인
                    for (int j = 0; j < 2; j++)
                    {
                        if (Positions[j] != firstPosition)
                        {
                            nowPosition = Positions[j];
                        }
                    }
                    // 4단계 동물 랜덤 업그레이드 (★동물 수 변경)
                    int RandomAnimal = Random.Range(0, 3);
                    Instantiate(Animals4[RandomAnimal], new Vector3(nowPosition.x, nowPosition.y + 0.46f, 0), Quaternion.identity);
                    // 동물 생성 가능한 땅 정보 업데이트
                    switch (firstPosition.x)
                    {
                        case -2.057f:
                            updateGround(0);
                            break;
                        case -1.592f:
                            updateGround(1);
                            break;
                        case -1.127f:
                            updateGround(2);
                            break;
                        case -0.669f:
                            updateGround(3);
                            break;
                        case -0.2154824f:
                            updateGround(4);
                            break;
                        case 0.2433135f:
                            updateGround(5);
                            break;
                        case 0.7021092f:
                            updateGround(6);
                            break;
                        case 1.167554f:
                            updateGround(7);
                            break;
                        case 1.629f:
                            updateGround(8);
                            break;
                        case 2.091795f:
                            updateGround(9);
                            break;

                    }
                    break;

                }
                else // 충돌한 동물이 없다면 제자리로
                {
                    this.transform.position = firstPosition;
                }
            }

        }
    }
    void updateGround(int n)
    {
        GameObject.Find("AnimalAdd").GetComponent<AnimalAdd>().GroundOK[n] = true;
        GameObject.Find("AnimalAdd").GetComponent<AnimalAdd>().isGroundNumber++;
    }
}