using System;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Mediation;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.Example
{
    public class RewardAd : MonoBehaviour ,IDisposable
    {
        IRewardedAd ad;
        string adUnitId = "Rewarded_Android";
        string gameId = "5029605";

        public Button _rewardBtn;

        void Start(){
            _ = InitServices();
            _rewardBtn.interactable = false;
        }
        public async Task InitServices()
        {
            try
            {
                InitializationOptions initializationOptions = new InitializationOptions();
                initializationOptions.SetGameId(gameId);
                await UnityServices.InitializeAsync(initializationOptions);

                InitializationComplete();
            }
            catch (Exception e)
            {
                InitializationFailed(e);
            }
        }

        public void SetupAd()
        {
            //Create
            ad = MediationService.Instance.CreateRewardedAd(adUnitId);

            //Subscribe to events
            ad.OnClosed += AdClosed;
            ad.OnClicked += AdClicked;
            ad.OnLoaded += AdLoaded;
            ad.OnFailedLoad += AdFailedLoad;
            ad.OnUserRewarded += UserRewarded;

            // Impression Event
            MediationService.Instance.ImpressionEventPublisher.OnImpression += ImpressionEvent;
        }

        public void Dispose() => ad?.Dispose();

        
        public async void ShowAd()
        {
            if (ad.AdState == AdState.Loaded)
            {
                try
                {
                    RewardedAdShowOptions showOptions = new RewardedAdShowOptions();
                    showOptions.AutoReload = true;
                    await ad.ShowAsync(showOptions);
                    AdShown();
                }
                catch (ShowFailedException e)
                {
                    AdFailedShow(e);
                }
            }
        }

        void InitializationComplete()
        {
            SetupAd();
            _ = LoadAd();
        }

        async Task LoadAd()
        {
            try
            {
                await ad.LoadAsync();
            }
            catch (LoadFailedException)
            {
                // We will handle the failure in the OnFailedLoad callback
            }
        }

        void InitializationFailed(Exception e)
        {
            Debug.Log("Initialization Failed: " + e.Message);
        }

        void AdLoaded(object sender, EventArgs e)
        {
            Debug.Log("Ad loaded");
            _rewardBtn.onClick.AddListener(ShowAd);
            _rewardBtn.interactable = true;
        }

        void AdFailedLoad(object sender, LoadErrorEventArgs e)
        {
            Debug.Log("Failed to load ad");
            Debug.Log(e.Message);
        }
        
        void AdShown()
        {
            Debug.Log("Ad shown!");
        }
        
        void AdClosed(object sender, EventArgs e)
        {
            Debug.Log("Ad has closed");
            // Execute logic after an ad has been closed.
        }

        void AdClicked(object sender, EventArgs e)
        {
            Debug.Log("Ad has been clicked");
            // Execute logic after an ad has been clicked.
        }
        
        void AdFailedShow(ShowFailedException e)
        {
            Debug.Log(e.Message);
        }

        void ImpressionEvent(object sender, ImpressionEventArgs args)
        {
            var impressionData = args.ImpressionData != null ? JsonUtility.ToJson(args.ImpressionData, true) : "null";
            Debug.Log("Impression event from ad unit id " + args.AdUnitId + " " + impressionData);
        }
        
        void UserRewarded(object sender, RewardEventArgs e)
        {
            Debug.Log($"Received reward: type:{e.Type}; amount:{e.Amount}");
            int activeBallCount = PlayerPrefs.GetInt("StartBallCount");
            activeBallCount+=1;
            Debug.Log(("Ball count " + activeBallCount));
            PlayerPrefs.SetInt("StartBallCount", activeBallCount);
        }

    }
}