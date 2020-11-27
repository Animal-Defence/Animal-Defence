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
        GameObject.Find("EnemyDeathManager").GetComponent<EnemyDeathManager>().setNewEnemyDeath();
        GameObject.Find("Player_Coin").GetComponent<Player_Coin>().setNowPlayerCoin();
        GameObject.Find("TestUGUI").GetComponent<TestUGUI>().setGamePlayCount();
        GameObject.Find("TestUGUI").GetComponent<TestUGUI>().setReadDeveloperInfo();
        GameObject.Find("TestUGUI").GetComponent<TestUGUI>().testUBUISetting();
    }

    

}
