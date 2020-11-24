using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedBtnClick : MonoBehaviour
{
    public Sprite XSprite;
    private Image myImage;

    private void Start()
    {
        myImage = GetComponent<Image>();
    }

    public void ChangeImage()
    {
        myImage.sprite = XSprite;
        
    }

}
