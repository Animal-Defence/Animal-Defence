using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

public class UnityAds : MonoBehaviour
{
    public static UnityAds instance;

    [HideInInspector]
    public ManageVariables vars;

    private bool isRewardAdsReady = false;

    public bool IsRewardAdsReady { get { return isRewardAdsReady; } }

    void OnEnable()
    {
        vars = Resources.Load("ManageVariablesContainer") as ManageVariables;
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGameOver == true && GameManager.instance.canShowAds == true)
        {
            //we want only one ad to be shown so we put condition that when i is 0 we show ad.
            if (GameManager.instance.gamesPlayed >= vars.showInterstitialAfter)
            {
                GameManager.instance.gamesPlayed = 0;

                //Use any1 admob or unity
                #if AdmobDef
                AdsManager.instance.ShowInterstitial();
                #endif

                ShowAd();
            }
        }
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
#if UNITY_ADS
        if (Advertisement.IsReady("rewardedVideo"))
            isRewardAdsReady = true;
        else if (!Advertisement.IsReady("rewardedVideo"))
            isRewardAdsReady = false;
#endif
    }

    public void ShowAd()
    {
#if UNITY_ADS
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
#endif
    }

    //use this function for showing reward ads
    public void ShowRewardedAd()
    {
#if UNITY_ADS
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
        else
        {
            Debug.Log("Ads not ready");
        }
#endif
    }

#if UNITY_ADS
private void HandleShowResult(ShowResult result)
{
    switch (result)
    {
        case ShowResult.Finished:
            Debug.Log("The ad was successfully shown.");

/*here we give 50 poinst as reward*/
            GameManager.instance.points += 5;
            GameManager.instance.Save();

            break;
        case ShowResult.Skipped:
            Debug.Log("The ad was skipped before reaching the end.");
            break;
        case ShowResult.Failed:
            Debug.LogError("The ad failed to be shown.");

            break;
    }
}
#endif

}
