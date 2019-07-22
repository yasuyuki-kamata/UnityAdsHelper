using UnityEditor;
using UnityEditor.Events;
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
			var unityMonetizationController = buttonGameObject.AddComponent<UnityMonetizationController>();
			UnityEventTools.AddPersistentListener(button.onClick, unityMonetizationController.ShowVideoAds);
		}
	}
}
