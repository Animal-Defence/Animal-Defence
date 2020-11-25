using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAttack : MonoBehaviour
{
    public GameObject Weaponpf; // 무기 프리팹
    private Rigidbody2D rigid;
    private Vector3 firstPosition; // 오브젝트 처음 위치 저장
    public float maxShotDelay; // 공격 속도(쏘는데 걸리는 시간, 낮을 수록 빠름)
    private float curShotDelay;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        // 오브젝트 처음 위치 저장
        firstPosition = this.transform.position;
    }

    void Update()
    {
        //Rigidbody2D rigid = weapon.GetComponent<Rigidbody2D>();
        //Attack();
        ShotDelay();
    }
    // 무기로 공격
    private void Attack()
    {
        float e_minLen = 2000.0f;
        float b_minLen = 2000.0f;
        int e_minIndex = 0;
        int b_minIndex = 0;
        if (curShotDelay < maxShotDelay)
            return;

        GameObject weapon = Instantiate(Weaponpf, firstPosition, transform.rotation); // 무기 생성
        Rigidbody2D rigid = weapon.GetComponent<Rigidbody2D>();

        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy"); // Enemy Tag로 적들 찾기
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss"); // Enemy Tag로 적들 찾기
        
        if (enemys.Length == 0 && bosses.Length == 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0); // 적이 없으면 위를 향해 
        }
        else
        {
            for (int i = 0; i < enemys.Length; i++) // Enemys
            {
                if (e_minLen >= (enemys[i].transform.position - gameObject.transform.position).sqrMagnitude)
                {
                    if (enemys[i].transform.position.y > 0)
                    {
                        e_minLen = (enemys[i].transform.position - gameObject.transform.position).sqrMagnitude;
                        e_minIndex = i;
                    }
                }
            }
            
            for (int i = 0; i < bosses.Length; i++) // Bosses
            {
                if (b_minLen >= (bosses[i].transform.position - gameObject.transform.position).sqrMagnitude)
                {
                    if (bosses[i].transform.position.y > 0)
                    {
                        b_minLen = (bosses[i].transform.position - gameObject.transform.position).sqrMagnitude;
                        b_minIndex = i;
                    }
                }
            }
            Vector2 target;
            if (e_minLen < b_minLen) // Enemy가 Boss보다 더 가까우면
            {
                target = enemys[e_minIndex].transform.position;
            }
            else
            {
                target = bosses[b_minIndex].transform.position;
            }
            Vector2 me = gameObject.transform.position;
            float angle = Mathf.Atan2(target.y - me.y, target.x - me.x) * Mathf.Rad2Deg;
            gameObject.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward); // 적을 향해 회전
        }
        rigid.AddRelativeForce(Vector2.up * 20, ForceMode2D.Impulse); // 공격

        curShotDelay = 0;
    }

    private void ShotDelay()
    {
        if (curShotDelay >= maxShotDelay)
        {
            curShotDelay = 0.0f;
            GameObject weapon = Instantiate(Weaponpf, firstPosition, transform.rotation); // 무기 생성
        }
        else
        {
            curShotDelay += Time.deltaTime;
        }
    }
}
