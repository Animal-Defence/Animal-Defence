﻿using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.SceneManagement;

public class LeaderboardiOSManager : MonoBehaviour
{

    public static LeaderboardiOSManager instance;

    public string leaderBoardID = "PROVIDE_YOUR_LEADERBOARD_ID_HERE";


    #region GAME_CENTER	

    /// <summary>
    /// Authenticates to game center.
    /// </summary>

    // *************************************************************New Code
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    // *************************************************************New Code

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void Awake()
    {
        MakeInstance();
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

    void Start()
    {
        AuthenticateToGameCenter();
    }


    public void AuthenticateToGameCenter()
    {
#if UNITY_IOS
        Social.localUser.Authenticate(success =>
                                        {
                                            if (success)
                                            {
                                                Debug.Log("Authentication successful");
                                            }
                                            else
                                            {
                                                Debug.Log("Authentication failed");
                                            }
                                        });
#endif
    }

    /// <summary>
    /// Reports the score on leaderboard.
    /// </summary>
    /// <param name="score">Score.</param>
    /// <param name="leaderboardID">Leaderboard I.</param>

    public static void ReportScore(long score, string leaderboardID)
    {

#if UNITY_EDITOR

        Debug.Log("Working");

#elif UNITY_IOS
    //Debug.Log("Reporting score " + score + " on leaderboard " + leaderboardID);
    Social.ReportScore(score, leaderboardID, success =>
		{
		    if (success)
		    {
			    Debug.Log("Reported score successfully");
		    }
		    else
		    {
			    Debug.Log("Failed to report score");
		    }

		    Debug.Log(success ? "Reported score successfully" : "Failed to report score"); Debug.Log("New Score:"+score);  
	    });
#endif
    }

    // *************************************************************New Code
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
#if UNITY_IOS
        if (Social.localUser.authenticated == true)
        {
            ReportScore(GameManager.instance.lastScore, leaderBoardID);
        }
#endif
    }
    // *************************************************************New Code

    /// <summary>
    /// Shows the leaderboard UI.
    /// </summary>

    public void ShowLeaderboard()
    {
#if UNITY_IOS
        Social.ShowLeaderboardUI();
#endif
    }

    #endregion
}