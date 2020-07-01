﻿using System;
using UnityEditor;
using UnityEditor.Advertisements;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace UnityAdsHelper.Editor
{
	public static class AdsButtonMenu
	{
		[MenuItem("Unity Ads Helper/Button/Create Video Ads Button")]
		private static void CreateInterstitialAdsButton()
		{
			CreateAdsButton("video");
		}

		[MenuItem("Unity Ads Helper/Button/Create Rewarded Ads Button")]
		private static void CreateRewardedAdsButton()
		{
			CreateAdsButton("rewardedVideo");
		}

		private static void CreateAdsButton(string placementId)
		{
			EditorApplication.ExecuteMenuItem("GameObject/UI/Button");
			var buttonGameObject = Selection.activeGameObject;
			var rectTransform = buttonGameObject.transform as RectTransform;
			if (rectTransform != null)
			{
				rectTransform.anchoredPosition = Vector2.zero;
				rectTransform.sizeDelta = new Vector2(200, 50);
			}
			var button = buttonGameObject.GetComponent<Button>();
			var unityAdsHelper = Object.FindObjectOfType<UnityAdsHelper>();
			GameObject helperGameObject;
			if (unityAdsHelper == null)
			{
				helperGameObject = new GameObject("UnityAdsHelper");
				unityAdsHelper = helperGameObject.AddComponent<UnityAdsHelper>();
			}
			else
			{
				helperGameObject = unityAdsHelper.gameObject;
			}
			var targetInfo = UnityEventBase.GetValidMethodInfo(
				unityAdsHelper, "ShowVideoAds",
				new Type[] { typeof(string) }
				);
			var action = Delegate.CreateDelegate(
				typeof(UnityAction<string>),
				unityAdsHelper,
				targetInfo,
				false
				) as UnityAction<string>;
			UnityEventTools.AddStringPersistentListener(button.onClick, action, placementId);

			// Change button text
			buttonGameObject.GetComponentInChildren<Text>().text = $"Show {placementId} ads";

			// Set selection
			Selection.activeGameObject = helperGameObject;
		}

#if ENABLE_CLOUD_SERVICES_ADS
		[MenuItem("Unity Ads Helper/Game ID/Apply Game IDs from connected Cloud Services")]
		private static void ApplyGameIds()
		{
			var unityAdsHelper = Object.FindObjectOfType<UnityAdsHelper>();
			var gameIdAppleAppStoreFromCloudServices = AdvertisementSettings.GetGameId(RuntimePlatform.IPhonePlayer);
			var gameIdGooglePlayFromCloudServices= AdvertisementSettings.GetGameId(RuntimePlatform.Android);
			if (gameIdAppleAppStoreFromCloudServices != unityAdsHelper.GameIdAppleAppStore ||
			    gameIdGooglePlayFromCloudServices != unityAdsHelper.GameIdGooglePlay)
			{
				if (EditorUtility.DisplayDialog(
					"Apply Game IDs from connected Cloud Services", $"Replace your Game IDs?\n\n" +
				    $"Apple AppStore\n{unityAdsHelper.GameIdAppleAppStore}=>{gameIdAppleAppStoreFromCloudServices}\n\n" +
				    $"GooglePlay\n{unityAdsHelper.GameIdGooglePlay}=>{gameIdGooglePlayFromCloudServices}",
					"Replace",
					"Cancel"))
				{
					unityAdsHelper.GameIdAppleAppStore = gameIdAppleAppStoreFromCloudServices;
					unityAdsHelper.GameIdGooglePlay = gameIdGooglePlayFromCloudServices;
				}
			}
		}
#endif
	}
}