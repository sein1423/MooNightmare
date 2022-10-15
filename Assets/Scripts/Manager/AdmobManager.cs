using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class AdmobManager : MonoBehaviour
{
    public static AdmobManager Instance;
    bool isTestMode = false;
    public bool isEndAds = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    /*
    private void Start()
    {
        InitAd();
        Invoke("Show", 10f);
        
    }

    private void InitAd()
    {
        string id = Debug.isDebugBuild ? test_unitID : unitID;

        screenAd = new InterstitialAd(id);

        AdRequest request = new AdRequest.Builder().Build();

        screenAd.LoadAd(request);
        screenAd.OnAdClosed += (sender, e) => Debug.Log("±¤°í°¡ ´ÝÈû");
        screenAd.OnAdLoaded += (sender, e) => Debug.Log("±¤°í ·Îµå");


    }

    public void Show()
    {
        StartCoroutine("ShowScreenAd");
    }

    private IEnumerator ShowScreenAd()
    {
        while (!screenAd.IsLoaded())
        {
            yield return null;
        }
        screenAd.Show();
    }*/


    void Start()
    {
        var requestConfiguration = new RequestConfiguration.Builder().build();

        MobileAds.SetRequestConfiguration(requestConfiguration);

        LoadBannerAd();
        LoadFrontAd();
        LoadRewardAd();
        LoadRewardAdGame();
    }

    void Update()
    {
        //FrontAdsBtn.interactable = frontAd.IsLoaded();
        //RewardAdsBtn.interactable = rewardAd.IsLoaded();
    }

    AdRequest GetAdRequest()
    {
        return new AdRequest.Builder().Build();
    }



    #region ¹è³Ê ±¤°í
    const string bannerTestID = "ca-app-pub-3940256099942544/6300978111";
    const string bannerID = "";
    BannerView bannerAd;


    void LoadBannerAd()
    {
        bannerAd = new BannerView(isTestMode ? bannerTestID : bannerID,
            AdSize.SmartBanner, AdPosition.Bottom);
        bannerAd.LoadAd(GetAdRequest());
        ToggleBannerAd(false);
    }

    public void ToggleBannerAd(bool b)
    {
        if (b) bannerAd.Show();
        else bannerAd.Hide();
    }
    #endregion



    #region Àü¸é ±¤°í
    const string frontTestID = "ca-app-pub-3940256099942544/8691691433";
    const string frontID = "ca-app-pub-7992386729167855/6540762859";
    InterstitialAd frontAd;


    void LoadFrontAd()
    {
        frontAd = new InterstitialAd(frontID);
        frontAd.LoadAd(GetAdRequest());
        frontAd.OnAdClosed += (sender, e) =>
        {
            Debug.Log("¾ÀÀÌµ¿");
            GameManager.Instance.goMain();
        };
    }

    public void ShowFrontAd()
    {
        frontAd.Show();
        LoadFrontAd();
    }
    #endregion



    #region ¸®¿öµå ±¤°í
    const string rewardTestID = "ca-app-pub-3940256099942544/5224354917";
    const string rewardID = "ca-app-pub-7992386729167855/4106171201";
    const string rewardStarID = "ca-app-pub-7992386729167855/4297742893";
    RewardedAd rewardAd;
    RewardedAd ScenerewardAd;

    void LoadRewardAd()
    {
        Debug.Log("in Load AD");
        rewardAd = new RewardedAd(rewardID);
        rewardAd.LoadAd(GetAdRequest());
        rewardAd.OnUserEarnedReward += (sender, e) =>
        {
            Debug.Log("±¤°í ¿Ï·á");
        };
    }

    public void ShowRewardAd()
    {
        rewardAd.Show();
        LoadRewardAd();
    }

    


    void LoadRewardAdGame()
    {
        Debug.Log("in Load AD Game");
        ScenerewardAd = new RewardedAd(rewardStarID);
        ScenerewardAd.LoadAd(GetAdRequest());
        ScenerewardAd.OnUserEarnedReward += (sender, e) =>
        {
            Debug.Log("end ad");
            GameManager.Instance.goToStage();
        };
    }

    public void ShowRewardAdGame()
    {
        Debug.Log("AD Game");
        ScenerewardAd.Show();
        Debug.Log("Load AD Game");
        LoadRewardAdGame();
    }
    #endregion
}
