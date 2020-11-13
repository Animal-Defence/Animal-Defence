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
        Attack();
        ShotDelay();
    }
    // 무기로 공격
    private void Attack()
    {
        float minLen = 2000.0f;
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
                    if (enemys[i].transform.position.y > 0)
                    {
                        minLen = (enemys[i].transform.position - gameObject.transform.position).sqrMagnitude;
                        minIndex = i;
                    }
                }
            }
            Vector2 target = enemys[minIndex].transform.position;
            Vector2 me = gameObject.transform.position;
            float angle = Mathf.Atan2(target.y - me.y, target.x - me.x) * Mathf.Rad2Deg;
            gameObject.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
        rigid.AddRelativeForce(Vector2.up * 15, ForceMode2D.Impulse); // 가까운 적을 향해 공격

        curShotDelay = 0;
    }

    private void ShotDelay()
    {
        curShotDelay += Time.deltaTime;
    }
}
