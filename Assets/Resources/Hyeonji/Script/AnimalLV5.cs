﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalLV5 : MonoBehaviour
{
    private Rigidbody2D rigid;
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