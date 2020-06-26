using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;
using UnityEngine.Monetization;

namespace UnityAdsHelper
{
	public class UnityAdsHelper : MonoBehaviour
	{
		[SerializeField] private string gameIdAppleAppStore = "";
		[SerializeField] private string gameIdGooglePlay = "";
		[SerializeField] private bool useAnotherGameIdForTest = false;
		[SerializeField] private string gameIdAppleAppStoreForTest = "";
		[SerializeField] private string gameIdGooglePlayForTest = "";
		[SerializeField] private bool initializeOnStart = true;
		[SerializeField] private bool enableTestMode = false;
		
		private string _gameId = "";
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
			_gameId = useAnotherGameIdForTest ? gameIdAppleAppStoreForTest : gameIdAppleAppStore;
#elif UNITY_ANDROID
			_gameId = useAnotherGameIdForTest ? gameIdGooglePlayForTest : gameIdGooglePlay; 
#endif
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
	}
}
