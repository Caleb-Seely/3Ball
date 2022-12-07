using System;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Mediation;
using UnityEngine;

namespace Unity.Example
{
    public class BannerAd : MonoBehaviour, IDisposable
    {
        IBannerAd ad;
        string adUnitId = "Banner_Android";
        string gameId = "5029605";

        void Start() {
            _ = InitServices();
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
            ad = MediationService.Instance.CreateBannerAd(
                adUnitId,
                BannerAdPredefinedSize.Banner.ToBannerAdSize(),
                BannerAdAnchor.TopCenter,
                Vector2.zero);

            //Subscribe to events
            ad.OnRefreshed += AdRefreshed;
            ad.OnClicked += AdClicked;
            ad.OnLoaded += AdLoaded;
            ad.OnFailedLoad += AdFailedLoad;
            
            // Impression Event
            MediationService.Instance.ImpressionEventPublisher.OnImpression += ImpressionEvent;
        }

        public void Dispose() => ad?.Dispose();

        
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
        }

        void AdFailedLoad(object sender, LoadErrorEventArgs e)
        {
            Debug.Log("Failed to load ad");
            Debug.Log(e.Message);
        }
        
        void AdRefreshed(object sender, LoadErrorEventArgs e)
        {
            Debug.Log("Refreshed ad");
            //Debug.Log(e.Message);
        }
        
        void AdClicked(object sender, EventArgs e)
        {
            Debug.Log("Ad has been clicked");
            // Execute logic after an ad has been clicked.
        }
        
        void ImpressionEvent(object sender, ImpressionEventArgs args)
        {
            var impressionData = args.ImpressionData != null ? JsonUtility.ToJson(args.ImpressionData, true) : "null";
            Debug.Log("Impression event from ad unit id " + args.AdUnitId + " " + impressionData);
        }

        public void AdHide(){
            Debug.Log("Destroy the banner Ad");
            ad.Dispose();
        }
        
        public void AdRestart(){
            _ = InitServices();
        }
    }
}