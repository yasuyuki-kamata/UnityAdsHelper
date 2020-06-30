using System;
using UnityEditor;
using UnityEditor.Events;
using UnityEngine.Events;
using UnityEngine.UI;

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
			var unityAdsHelper = buttonGameObject.AddComponent<UnityAdsHelper>();
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
			UnityEventTools.AddStringPersistentListener(button.onClick, action, "rewardedVideo");

			buttonGameObject.GetComponentInChildren<Text>().text = "Show video ads";
		}
	}
}