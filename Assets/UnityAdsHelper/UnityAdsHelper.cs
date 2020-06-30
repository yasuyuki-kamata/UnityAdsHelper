using System;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

namespace UnityAdsHelper
{
	public class UnityAdsHelper : MonoBehaviour, IUnityAdsListener
	{
		private const string VideoPlacement = "video";
		private const string RewardedVideoPlacement = "rewardedVideo";
		
#pragma warning disable 0414
		[SerializeField] private string gameIdAppleAppStore = "3219868";
		[SerializeField] private string gameIdGooglePlay = "3219869";
		[SerializeField] private bool useAnotherGameIdForDevelopment = false;
		[SerializeField] private string gameIdAppleAppStoreForDevelopment = "3219868";
		[SerializeField] private string gameIdGooglePlayForDevelopment = "3219869";
		[SerializeField] private bool initializeOnStart = true;
		[SerializeField] private bool enableTestMode;
#pragma warning restore 0414
		
#pragma warning disable 0649
		[SerializeField] private UnityEvent onAdsReady;
		[SerializeField] private UnityEvent onAdsDidError;
		[SerializeField] private UnityEvent onAdsDidStart;
		[SerializeField] private UnityEvent onAdsFinished;
		[SerializeField] private UnityEvent onAdsSkipped;
		[SerializeField] private UnityEvent onAdsFailed;
#pragma  warning restore 0649

		private string _gameId;

		private void Start()
		{
			if (initializeOnStart)
			{
				InitUnityAds();
			}
		}

		/// <summary>
		/// Initialize Unity Ads
		///
		/// Initialize with selected platform's game id, and AddListener
		/// </summary>
		public void InitUnityAds()
		{
			if (!Advertisement.isSupported || Advertisement.isInitialized) return;
#if UNITY_IOS
			_gameId = useAnotherGameIdForDevelopment && Debug.isDebugBuild ? gameIdAppleAppStoreForDevelopment : gameIdAppleAppStore;
#elif UNITY_ANDROID
			_gameId = useAnotherGameIdForDevelopment && Debug.isDebugBuild ? gameIdGooglePlayForDevelopment : gameIdGooglePlay;
#else
			Debug.Log("else");
#endif
			if (string.IsNullOrEmpty(_gameId))
			{
				Debug.LogWarning($"Unity Ads game id is empty. Please input your game id.");
				return;
			}

			Advertisement.Initialize(_gameId, enableTestMode);

			Advertisement.AddListener(this);
		}

		/// <summary>
		/// Show video ads using "video" placement
		/// 
		/// "video" is a non-rewarded Placement that serves Video ad and Playable ad formats, and allows users to skip the ad after 5 seconds.
		/// </summary>
		public void ShowVideoAds()
		{
			if (!Advertisement.IsReady(VideoPlacement)) return;
			Advertisement.Show(VideoPlacement);
		}
		
		/// <summary>
		/// Show ads with placement id
		/// </summary>
		/// <param name="placementId">Placement ID</param>
		public void ShowVideoAds(string placementId)
		{
			if (!Advertisement.IsReady(placementId)) return;
			Advertisement.Show(placementId);
		}

		/// <summary>
		/// Show rewarded video ads using "rewardedVideo" placement
		/// 
		/// "rewardedVideo" is a rewarded Placement that serves Video ad and Playable ad formats, but does not allow skipping.
		/// </summary>
		public void ShowRewardedVideoAds()
		{
			if (!Advertisement.IsReady(RewardedVideoPlacement)) return;
			Advertisement.Show(RewardedVideoPlacement);
		}
		
		private void OnDestroy()
		{
			Advertisement.RemoveListener(this);
		}
		
#pragma warning disable 1633
#pragma region Properties
		public string GameIdGooglePlay
		{
			get => gameIdGooglePlay;
			set => gameIdGooglePlay = value;
		}

		public string GameIdAppleAppStore
		{
			get => gameIdAppleAppStore;
			set => gameIdAppleAppStore = value;
		}

		public bool UseAnotherGameIdForDevelopment
		{
			get => useAnotherGameIdForDevelopment;
			set => useAnotherGameIdForDevelopment = value;
		}

		public string GameIdAppleAppStoreForDevelopment
		{
			get => gameIdAppleAppStoreForDevelopment;
			set => gameIdAppleAppStoreForDevelopment = value;
		}

		public string GameIdGooglePlayForDevelopment
		{
			get => gameIdGooglePlayForDevelopment;
			set => gameIdGooglePlayForDevelopment = value;
		}

		public bool InitializeOnStart
		{
			get => initializeOnStart;
			set => initializeOnStart = value;
		}

		public bool EnableTestMode
		{
			get => enableTestMode;
			set => enableTestMode = value;
		}
		
#pragma endregion
#pragma warning restore 1633
		
#pragma warning disable 1633
#pragma region implemented from IUnityAdsListener
		public void OnUnityAdsReady(string placementId)
		{
			onAdsReady.Invoke();
		}

		public void OnUnityAdsDidError(string message)
		{
			onAdsDidError.Invoke();
		}

		public void OnUnityAdsDidStart(string placementId)
		{
			onAdsDidStart.Invoke();
		}

		public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
		{
			switch (showResult)
			{
				case ShowResult.Finished:
					onAdsFinished.Invoke();
					break;
				case ShowResult.Skipped:
					onAdsSkipped.Invoke();
					break;
				case ShowResult.Failed:
					onAdsFailed.Invoke();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(showResult), showResult, null);
			}
		}
#pragma endregion
#pragma warning restore 1633
	}
}