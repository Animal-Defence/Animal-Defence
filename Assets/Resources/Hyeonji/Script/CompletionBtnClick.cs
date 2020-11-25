using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletionBtnClick : MonoBehaviour
{
    public Button SelectedBtn1;
    public Button SelectedBtn2;
    public Button SelectedBtn3;
    private string AnimalName1;
    private string AnimalName2;
    private string AnimalName3;

    public void SettingCompleted()
    {
        AnimalName1 = AnimalNumberFX(SelectedBtn1.image.sprite.name);
        AnimalName2 = AnimalNumberFX(SelectedBtn2.image.sprite.name);
        AnimalName3 = AnimalNumberFX(SelectedBtn3.image.sprite.name);
        // 데이터 보내기
    }

    // SelectedBtn에 있는 이미지 이름을 숫자로 반환
    private string AnimalNumberFX(string AnimalImg)
    {
        string name = "";
        switch (AnimalImg)
        {
            case "dogImg":
                name = "dog"; 
                return name;
            case "elephantImg":
                name = "elephant";
                return name;
            case "giraffeImg":
                name = "giraffe";
                return name;
            case "hippoImg":
                name = "hippo";
                return name;
            case "monkeyImg":
                name = "monkey";
                return name;
            case "pandaImg":
                name = "panda";
                return name;
            case "parrotImg":
                name = "parrot";
                return name;
            case "penguinImg":
                name = "penguin";
                return name;
            case "pigImg":
                name = "pig";
                return name;
            case "snakeImg":
                name = "snake";
                return name;
        }
        return name;
    }
}
