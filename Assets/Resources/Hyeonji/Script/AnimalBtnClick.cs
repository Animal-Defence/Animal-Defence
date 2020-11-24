using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalBtnClick : MonoBehaviour
{
    public Sprite AnimalSprite;
    public Button SelectedBtn1;
    public Button SelectedBtn2;
    public Button SelectedBtn3;

    public void ChangeImage()
    {
        if (SelectedBtn1.image.sprite.name == "XbtnImg")
        {
            SelectedBtn1.image.sprite = AnimalSprite;
        }
        else if(SelectedBtn2.image.sprite.name == "XbtnImg")
        {
            SelectedBtn2.image.sprite = AnimalSprite;
        }
        else if(SelectedBtn3.image.sprite.name == "XbtnImg")
        {
            SelectedBtn3.image.sprite = AnimalSprite;
        }
        else
        {

        }
    }
}
