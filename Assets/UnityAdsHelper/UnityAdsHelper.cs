using UnityEngine;
using UnityEngine.Advertisements;

namespace UnityAdsHelper
{
	public class UnityAdsHelper : MonoBehaviour
	{
		#pragma warning disable 0414
		[SerializeField] private string gameIdAppleAppStore = "3219868";
		[SerializeField] private string gameIdGooglePlay = "3219869";
		[SerializeField] private bool useAnotherGameIdForDevelopment = false;
		[SerializeField] private string gameIdAppleAppStoreForDevelopment = "3219868";
		[SerializeField] private string gameIdGooglePlayForDevelopment = "3219869";
		[SerializeField] private bool initializeOnStart = true;
		[SerializeField] private bool enableTestMode = false;
		#pragma warning restore 0414
		
		private string _gameId;
		private UnityAdsListener _listener;

		private void Start()
		{
			if (initializeOnStart)
			{
				InitUnityAds();
			}
		}

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

			_listener = GetComponent<UnityAdsListener>();
			if (_listener != null)
			{
				Advertisement.AddListener(_listener);
			}
		}
		
		public void ShowVideoAds(string placementId)
		{
			if (!Advertisement.IsReady(placementId)) return;
			Advertisement.Show(placementId);
		}

		private void OnDestroy()
		{
			Advertisement.RemoveListener(_listener);
		}
		
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
	}
}