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
        Debug.DrawRay(rigid.position, Vector3.down * 350, new Color(1, 0, 0));
        Attack();
        ShotDelay();
    }

    // 무기로 공격
    private void Attack()
    {
        if (curShotDelay < maxShotDelay)
            return;

        GameObject weapon = Instantiate(Weaponpf, firstPosition, transform.rotation);
        Rigidbody2D rigid = weapon.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 500, ForceMode2D.Impulse);

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
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        this.transform.position = mousePosition;
    }

    // 마우스를 놓을 때
    private void OnMouseUp()
    {
        // RaycastAll 아래로 확인
        RaycastHit2D[] rayDown = Physics2D.RaycastAll(rigid.position, Vector3.down, 350);
        this.transform.position = firstPosition;
    }
}
