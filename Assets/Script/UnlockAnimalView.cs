using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UnlockAnimalView : MonoBehaviour
{
    public GameObject unlockAnimalView;
    public Image AnimalImage;
    public Text subTextB;
    public Text subTextW;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            unlockAnimalView.SetActive(false);
        }
    }

    public void setImageAndText(int index)
    {
        var missionData = DataManager.GetInstance().GetData<MissionData>(index);
        subTextB.text = "새로운 동물 " + missionData.animal_name + " 해금!";
        subTextW.text = subTextB.text;
        AnimalImage.sprite = Resources.Load<Sprite>(string.Format("Animals/{0}", missionData.animal_sprite_name));
    }



}
