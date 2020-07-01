using System;
using UnityEditor;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace UnityAdsHelper.Editor
{
	public class AdsButtonMenu
	{
		[MenuItem("Unity Ads Helper/Create Ads Button")]
		private static void CreateAdsButton()
		{
			EditorApplication.ExecuteMenuItem("GameObject/UI/Button");
			var buttonGameObject = Selection.activeGameObject;
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
			UnityEventTools.AddStringPersistentListener(button.onClick, action, "video");

			// Change button text
			buttonGameObject.GetComponentInChildren<Text>().text = "Show video ads";

			// Set selection
			Selection.activeGameObject = helperGameObject;
		}
	}
}