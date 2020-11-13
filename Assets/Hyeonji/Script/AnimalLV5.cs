using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalLV5 : MonoBehaviour
{
    public GameObject Weaponpf; // 무기 프리펩
    private Rigidbody2D rigid;
    public float maxShotDelay; // 총알 쏘는 시간
    public float curShotDelay;
    private Vector3 firstPosition; // 오브젝트 처음 위치 저장
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
        this.transform.position = firstPosition;
    }
}
