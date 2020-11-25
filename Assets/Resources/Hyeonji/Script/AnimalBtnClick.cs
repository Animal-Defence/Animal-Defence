﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalBtnClick : MonoBehaviour
{
    public Sprite AnimalSprite;
    public Button SelectedBtn1;
    public Button SelectedBtn2;
    public Button SelectedBtn3;
    public bool isSelected = true; // 동물이 등록 할 수 있는 상태인지

    // 동물 버튼 클릭시 SelectedBtn에 등록
    public void ChangeImage()
    {
        if (SelectedBtn1.image.sprite.name == "XbtnImg" && isSelected)
        {
            SelectedBtn1.image.sprite = AnimalSprite;
            isSelected = false;
        }
        else if(SelectedBtn2.image.sprite.name == "XbtnImg" && isSelected)
        {
            SelectedBtn2.image.sprite = AnimalSprite;
            isSelected = false;
        }
        else if(SelectedBtn3.image.sprite.name == "XbtnImg" && isSelected)
        {
            SelectedBtn3.image.sprite = AnimalSprite;
            isSelected = false;
        }
        else
        {

        }
    }
}
