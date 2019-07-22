using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Monetization;

namespace UnityAdsHelper
{
	public class UnityMonetizationController : MonoBehaviour
	{
		public UnityAdsSettings unityAdsSettings;
		public UnityAdsListener unityAdsListener;

		private string _gameId = "";

		private void Start()
		{
			InitMonetization();
		}

		private void InitMonetization()
		{
			if (!Monetization.isSupported || Monetization.isInitialized) return;
			if (unityAdsSettings != null)
			{
#if UNITY_IOS
				_gameId = unityAdsSettings.gameIdAppleAppStore ?? "";
#elif UNITY_ANDROID
				_gameId = UnityAdsSettings.gameIdGooglePlay ?? "";
#endif
				Monetization.Initialize(_gameId, unityAdsSettings.isTestMode);
			}

			if (unityAdsListener != null) Advertisement.AddListener(unityAdsListener);
		}

		public void ShowVideoAds()
		{
			if (!Monetization.IsReady(unityAdsSettings.rewardedVideoPlacementId)) return;
			var placementContent = (ShowAdPlacementContent) Monetization.GetPlacementContent(unityAdsSettings.rewardedVideoPlacementId);
			placementContent.Show();
		}
	}
}
