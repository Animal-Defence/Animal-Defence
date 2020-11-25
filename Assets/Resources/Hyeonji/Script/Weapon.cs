using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float speed = 0.1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적이랑 부딪히거나 화면 밖으로 나가면 무기 삭제
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Finish")
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        //Attack();
    }

    void Attack()
    {
        float e_minLen = 2000.0f;
        float b_minLen = 2000.0f;
        int e_minIndex = 0;
        int b_minIndex = 0;

        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy"); // Enemy Tag로 적들 찾기
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss"); // Enemy Tag로 적들 찾기

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();

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
            //Vector2 me = gameObject.transform.position;
            //float angle = Mathf.Atan2(target.y - me.y, target.x - me.x) * Mathf.Rad2Deg;
            //gameObject.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward); // 적을 향해 회전
            transform.position = Vector3.Lerp(transform.position, target, speed);
        }

    }
}
