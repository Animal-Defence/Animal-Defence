﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GoMissionMenuBtn : MonoBehaviour
{
   public GameObject MissionMenuView;
   
   public void onClickGoMissionMenuBtn()
    {
        MissionMenuView.SetActive(true);
        Time.timeScale = 0.0f;
        //이제 미션 메뉴를 누를때마다 부르는게 아니라 스테이지 고르는 씬에 입장했을때만 실행되도록
        //게임끝나면 메인화면으로 넘어가게? 스테이지 선택으로 넘어가게?
        SettingMissionData();
    }

   public void SettingMissionData()
    {
        GameObject.Find("EnemyDeathManager").GetComponent<EnemyDeathManager>().setNewEnemyDeath();
        GameObject.Find("Player_Coin").GetComponent<Player_Coin>().setNowPlayerCoin();
        GameObject.Find("TestUGUI").GetComponent<TestUGUI>().setGamePlayCount();
        GameObject.Find("TestUGUI").GetComponent<TestUGUI>().setReadDeveloperInfo();
        GameObject.Find("TestUGUI").GetComponent<TestUGUI>().testUBUISetting();
    }

}
