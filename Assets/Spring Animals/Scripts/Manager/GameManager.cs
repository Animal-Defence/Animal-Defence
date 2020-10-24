using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
/// <summary>
/// This script helps in saving and loading data in device
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameData data;

    //data which is not stored on device but refered while game is on
    public bool isGameOver = false;
    public int currentScore , lastScore , currentCoins;
    public bool restart = false;
    public int gamesPlayed = 0;

    //data to store on device
    public bool isGameStartedFirstTime;
    public bool isMusicOn;
    public int hiScore, coins;
    public bool canShowAds;//when noAds is false we show ads and when its true we dont show it
    public bool showRate;
    public bool[] skinUnlocked;
    public int selectedSkin;

    void Awake()
    {
        MakeInstance();
        InitializeVariables();
    }

    void MakeInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    //we initialize variables here
    void InitializeVariables()
    {
        //first we load any data is avialable
        Load();
        if (data != null)
        {
            isGameStartedFirstTime = data.getIsGameStartedFirstTime();
        }
        else
        {
            isGameStartedFirstTime = true;
        }
        if (isGameStartedFirstTime)
        {
            //when game is started for 1st time on device we set the initial values
            isGameStartedFirstTime = false;
            hiScore = 0;
            isMusicOn = true;
            canShowAds = true;
            showRate = true;
            coins = 100;
            skinUnlocked = new bool[10]; //we have 10 characters
            skinUnlocked[0] = true;
            for (int i = 1; i < skinUnlocked.Length; i++)
            {
                skinUnlocked[i] = false;
            }
            selectedSkin = 0;

            data = new GameData();

            //storing data
            data.setIsGameStartedFirstTime(isGameStartedFirstTime);
            data.setIsMusicOn(isMusicOn);
            data.setHiScore(hiScore);
            data.setCanShowAds(canShowAds);
            data.setShowRate(showRate);
            data.setPoints(coins);
            data.setSkinUnlocked(skinUnlocked);
            data.setSelectedSkin(selectedSkin);
            Save();
            Load();
        }
        else
        {
            //getting data
            isGameStartedFirstTime = data.getIsGameStartedFirstTime();
            isMusicOn = data.getIsMusicOn();
            hiScore = data.getHiScore();
            canShowAds = data.getCanShowAds();
            showRate = data.getShowRate();
            coins = data.getPoints();
            selectedSkin = data.getSelectedSkin();
            skinUnlocked = data.getSkinUnlocked();
        }
    }

    //method to save data
    public void Save()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + "/GameInfo.dat");
            if (data != null)
            {
                data.setIsGameStartedFirstTime(isGameStartedFirstTime);
                data.setHiScore(hiScore);
                data.setIsMusicOn(isMusicOn);
                data.setCanShowAds(canShowAds);
                data.setShowRate(showRate);
                data.setPoints(coins);
                data.setSkinUnlocked(skinUnlocked);
                data.setSelectedSkin(selectedSkin);
                bf.Serialize(file, data);
            }
        }
        catch (Exception e)
        { }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

    //method to load data
    public void Load()
    {
        FileStream file = null;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + "/GameInfo.dat", FileMode.Open);//here we get saved file
            data = (GameData)bf.Deserialize(file);
        }
        catch (Exception e) { }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }


    public void ResetGameManager()
    {
        //when game is started for 1st time on device we set the initial values
        isGameStartedFirstTime = false;
        hiScore = 0;
        isMusicOn = true;
        canShowAds = true;
        showRate = true;
        coins = 100;
        skinUnlocked = new bool[10]; //we have 10 characters
        skinUnlocked[0] = true;
        for (int i = 1; i < skinUnlocked.Length; i++)
        {
            skinUnlocked[i] = false;
        }
        selectedSkin = 0;

        data = new GameData();

        //storing data
        data.setIsGameStartedFirstTime(isGameStartedFirstTime);
        data.setIsMusicOn(isMusicOn);
        data.setHiScore(hiScore);
        data.setCanShowAds(canShowAds);
        data.setShowRate(showRate);
        data.setPoints(coins);
        data.setSkinUnlocked(skinUnlocked);
        data.setSelectedSkin(selectedSkin);
        Save();
        Load();

        Debug.Log("GameManager Reset");
    }

}

[Serializable]
class GameData
{
    private bool isGameStartedFirstTime;
    private bool isMusicOn;
    private int hiScore, coins;
    private bool canShowAds;
    private bool showRate;
    private bool[] skinUnlocked;
    private int selectedSkin;

    //is game started 1st time
    public void setIsGameStartedFirstTime(bool isGameStartedFirstTime)
    {
        this.isGameStartedFirstTime = isGameStartedFirstTime;
    }

    public bool getIsGameStartedFirstTime()
    {
        return isGameStartedFirstTime;
    }

    //ads
    public void setCanShowAds(bool canShowAds)
    {
        this.canShowAds = canShowAds;
    }

    public bool getCanShowAds()
    {
        return canShowAds;
    }

    //rate
    public void setShowRate(bool showRate)
    {
        this.showRate = showRate;
    }

    public bool getShowRate()
    {
        return showRate;
    }

    //music
    public void setIsMusicOn(bool isMusicOn)
    {
        this.isMusicOn = isMusicOn;
    }

    public bool getIsMusicOn()
    {
        return isMusicOn;
    }

    //hi score 
    public void setHiScore(int hiScore)
    {
        this.hiScore = hiScore;
    }

    public int getHiScore()
    {
        return hiScore;
    }

    //coins
    public void setPoints(int coins)
    {
        this.coins = coins;
    }

    public int getPoints()
    {
        return coins;
    }

    //skin unlocked
    public void setSkinUnlocked(bool[] skinUnlocked)
    {
        this.skinUnlocked = skinUnlocked;
    }

    public bool[] getSkinUnlocked()
    {
        return this.skinUnlocked;
    }

    //selectedSkin
    public void setSelectedSkin(int selectedSkin)
    {
        this.selectedSkin = selectedSkin;
    }

    public int getSelectedSkin()
    {
        return this.selectedSkin;
    }
}