﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;


public class Player_Coin : MonoBehaviour
{

    public static int coin_score = 0; // 모두 공유.
    public Text coin_text_obj;
    //public static GameInfo gameInfo;

    public void setNowPlayerCoin()
    {
        //데이터를 읽어온다.
        

        var json = PlayerPrefs.GetString("game_info");//파일 이미 만들어져 있기 때문에 null처리안함
        TestUGUI.gameInfo = JsonConvert.DeserializeObject<GameInfo>(json);
        var foundMissionInfo = TestUGUI.gameInfo.missionInfos.Find(x => x.id == 0);
        if (foundMissionInfo != null)//이미 있다.
        {
            int index = TestUGUI.gameInfo.missionInfos.FindIndex(x => x.id == 0);
            TestUGUI.gameInfo.missionInfos[index].doingVal = coin_score;
        }
        else//아직없다.
        {
            TestUGUI.gameInfo.missionInfos.Add(new MissionInfo(0, coin_score));//없을경우 새로 만들어 넣는다.
        }
        var gameInfoJson = JsonConvert.SerializeObject(TestUGUI.gameInfo);//json을 storing형태로 저장.
        PlayerPrefs.SetString("game_info", gameInfoJson);
        PlayerPrefs.Save();

    }

}
