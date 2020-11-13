using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using System;
using System.IO;
using Newtonsoft.Json;

public class TestUGUI : MonoBehaviour
{

    public MissionListItem listItemPrefabs;
    public RectTransform contents;
    public static GameInfo gameInfo;
    private List<MissionListItem> missionListItems = new List<MissionListItem>();

    void Start()
    {
        //데이터 삭제
        //PlayerPrefs.DeleteAll();
        
        
        //데이터 로드
        DataManager.GetInstance().LoadData<MissionData>("Data/mission_data");
        //var data = DataManager.GetInstance().GetData<MissionData>(0);
        //Debug.LogFormat("{0} {1} {2}", data.id, data.sprite_name, data.mission_name);


        //info를 로드합니다
        var json = PlayerPrefs.GetString("game_info");
        if (string.IsNullOrEmpty(json)) // 저장된 파일이 없을경우 True반환
        {
            TestUGUI.gameInfo = new GameInfo();
            TestUGUI.gameInfo.missionInfos.Add(new MissionInfo(0, 100));
            var gameInfoJson = JsonConvert.SerializeObject(TestUGUI.gameInfo);
            PlayerPrefs.SetString("game_info", gameInfoJson);
        }
        else
        {
            Debug.LogFormat("{0}", json);
            TestUGUI.gameInfo = JsonConvert.DeserializeObject<GameInfo>(json);

        }


        for (int i = 0; i < 5; i++)
        {
            var listItem = this.CreateListItem();
            missionListItems.Add(listItem);
            listItem.btnClaim.onClick.AddListener(() =>
            {
                this.Claim(listItem.id);
            });

            var missionData = DataManager.GetInstance().GetData<MissionData>(i);
            var MissionName = string.Format(missionData.mission_name, missionData.goal_val.ToString("#,###"));

            if (TestUGUI.gameInfo != null)
            {
                var foundMission = TestUGUI.gameInfo.missionInfos.Find(x => x.id == missionData.id);
                if (foundMission != null)
                {
                    listItem.Init(foundMission.id, missionData.sprite_name, MissionName,missionData.animal_name, missionData.goal_val, foundMission.doingVal);
                }
                else
                {
                    listItem.Init(missionData.id, missionData.sprite_name, MissionName, missionData.animal_name, missionData.goal_val);
                }
            }
            else
            {
                listItem.Init(missionData.id, missionData.sprite_name, MissionName, missionData.animal_name, missionData.goal_val);
            }



        }


    }


    private void Claim(int id) // 클릭이벤트
    {
        var missionData = DataManager.GetInstance().GetData<MissionData>(id);
        var foundMissionInfo = TestUGUI.gameInfo.missionInfos.Find(x => x.id == id);
    
        if(foundMissionInfo != null)
        {
            if(foundMissionInfo.doingVal == missionData.goal_val)
            {
                //해당 리스트 아이템의 버튼 상태를 바꿔준다.
                var foundListIten = missionListItems.Find(x => x.id == foundMissionInfo.id);
                if(foundListIten != null)
                {
                    foundListIten.binderCliam.ChangeState(UIBinder_BtnCliam.eBtnState.Success);
                }
            
            
            }
        }
    
    }

    private MissionListItem CreateListItem()
    {
        var listItemGo = Instantiate(this.listItemPrefabs);
        listItemGo.transform.SetParent(contents, false);
        return listItemGo.GetComponent<MissionListItem>();
    }

}
