using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalAdd : MonoBehaviour
{
    
    public Button AnimalAddButton; // Animal+버튼
    public Transform []Animals; // Animals 프리팹
    private float[] GroundX = { 90.0f, 190.0f, 290.0f, 390.0f, 490.0f, 590.0f, 690.0f, 790.0f, 890.0f, 990.0f }; // Ground 위치 x좌표
    //private int[] GroundY; // Ground 위치 y좌표(1레벨 기준)
    public bool[] GroundOK = { true, true, true, true, true, true, true, true, true, true }; // Ground 위에 동물 생성 가능한지 확인하기 위한 배열
    public int isGroundNumber = 10;
    // Animal+버튼 OnClick 이벤트
    void AnimalAddButtonClick()
    {
        // 랜덤한 동물 (★동물 수 변경)
        int RandomAnimal = Random.Range(0, 3);
        // 랜덤한 위치
        int RandomX = Random.Range(0, 10);
        if(GroundOK[RandomX] == true) // 생성 가능한 Ground이면
        {
            Instantiate(Animals[RandomAnimal], new Vector3(GroundX[RandomX], 290, 0), Quaternion.identity);
            GroundOK[RandomX] = false;
            isGroundNumber--;
        }
        else // 아니면 다른 생성 가능한 Ground에 생성
        {
            if (isGroundNumber == 0) // 생성 가능한 Ground 없음
            {
                Debug.Log("생성불가능");
            }
            else
            {
                while (true)
                {
                    RandomX = Random.Range(0, 10);
                    if (GroundOK[RandomX] == true)
                    { // 생성 가능한 Ground발견
                        isGroundNumber--;
                        break;
                    }
                }
                Instantiate(Animals[RandomAnimal], new Vector3(GroundX[RandomX], 290, 0), Quaternion.identity);
                GroundOK[RandomX] = false;
            }
        }
    }

    void Start()
    {
        // Animal+버튼 OnCLick 이벤트 설정
        AnimalAddButton = this.transform.GetComponent<Button>();
        if (AnimalAddButton)
        {
            AnimalAddButton.onClick.AddListener(AnimalAddButtonClick);
        }
        else
        {
            Debug.Log("AddButtonMissing");
        }
    }

}
