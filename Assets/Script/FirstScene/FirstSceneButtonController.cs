﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class FirstSceneButtonController : MonoBehaviour
{

    public void onClickStartGameBtn()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void onClickClearDataBtn()
    {
        PlayerPrefs.DeleteAll();
    }
    public void onClick_DeveloperInfo_Btn()
    {
        readDeveloperInfo();
    }

    public void readDeveloperInfo()
    {
        var json = PlayerPrefs.GetString("game_info");//파일 이미 만들어져 있기 때문에 null처리안함
        TestUGUI.gameInfo = JsonConvert.DeserializeObject<GameInfo>(json);
        var foundMissionInfo = TestUGUI.gameInfo.missionInfos.Find(x => x.id == 4);
        if (foundMissionInfo == null)//이미 있다.
        {
            TestUGUI.gameInfo.missionInfos.Add(new MissionInfo(4, 1));//없을경우 새로 만들어 넣는다.
            var gameInfoJson = JsonConvert.SerializeObject(TestUGUI.gameInfo);//json을 storing형태로 저장.
            PlayerPrefs.SetString("game_info", gameInfoJson);
            PlayerPrefs.Save();
        }
    }
}