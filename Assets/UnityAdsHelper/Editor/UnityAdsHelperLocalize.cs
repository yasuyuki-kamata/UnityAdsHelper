using UnityEditor.Localization.Editor;

[assembly: Localization]
namespace UnityAdsHelper.Editor
{
	public static class UnityAdsHelperLocalize
	{
		public static string L10N (string str)
		{
			return Localization.Tr(str);
		}

	}
}