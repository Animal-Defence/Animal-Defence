using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoMissionMenuBtn : MonoBehaviour
{
   public GameObject MissionMenuView;

   public void onClickGoMissionMenuBtn()
    {
        MissionMenuView.SetActive(true);
        Time.timeScale = 0.0f;
    }
}
