using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

#if GooglePlayDef
using GooglePlayGames; 
using UnityEngine.SocialPlatforms;
#endif 

public class GooglePlayManager : MonoBehaviour
{

    public static GooglePlayManager singleton;

    private AudioSource sound;

    [HideInInspector]
    public ManageVariables vars;

    void OnEnable()
    {
        vars = Resources.Load("ManageVariablesContainer") as ManageVariables;
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void Awake()
    {
        MakeInstance();
    }

    void MakeInstance()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
        }
        else
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    // Use this for initialization
    void Start()
    {
        sound = GetComponent<AudioSource>();
#if GooglePlayDef
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                
            }
        });
#endif

    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        ReportScore(GameManager.instance.lastScore);
    }

    public void OpenLeaderboardsScore()
    {
#if GooglePlayDef
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(vars.leaderBoardID);
        }
#endif
    }

    void ReportScore(int score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, vars.leaderBoardID, (bool success) => { });
        }
    }

}