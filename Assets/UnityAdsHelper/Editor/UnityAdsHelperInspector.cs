using UnityEditor;
using UnityEngine;

namespace UnityAdsHelper.Editor
{
    [CustomEditor(typeof(UnityAdsHelper))]
    public class UnityAdsHelperInspector : UnityEditor.Editor
    {
	    private const string OperateDashboardUrl = "https://dashboard.unity3d.com";
        private const string UnityAdsKnowledgeBaseUrl = "https://unityads.unity3d.com/help/index";
        private const string UnityAdsForumUrl = "https://forum.unity.com/forums/unity-ads.67/";
        
        private void OnEnable()
        {
        }

        public override void OnInspectorGUI()
        {
	        var helper = target as UnityAdsHelper;
	        if (helper == null) return;
	        
	        EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Dashboard")) Help.BrowseURL(OperateDashboardUrl);
            if (GUILayout.Button("Docs")) Help.BrowseURL(UnityAdsKnowledgeBaseUrl);
            if (GUILayout.Button("Forum")) Help.BrowseURL(UnityAdsForumUrl);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Game ID", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            helper.GameIdAppleAppStore = EditorGUILayout.TextField("Apple AppStore", helper.GameIdAppleAppStore);
            helper.GameIdGooglePlay = EditorGUILayout.TextField("GooglePlay", helper.GameIdGooglePlay);
            
            EditorGUILayout.Space();
            EditorGUI.indentLevel--;

            helper.UseAnotherGameIdForDevelopment = EditorGUILayout.Toggle("Use Another Game ID for Development", helper.UseAnotherGameIdForDevelopment);
            
            if (helper.UseAnotherGameIdForDevelopment)
            {
	            EditorGUILayout.LabelField("Game ID (Development)", EditorStyles.boldLabel);
	            EditorGUI.indentLevel++;
	            helper.GameIdAppleAppStoreForDevelopment = EditorGUILayout.TextField("Apple AppStore (Development)", helper.GameIdAppleAppStoreForDevelopment);
	            helper.GameIdGooglePlayForDevelopment = EditorGUILayout.TextField("GooglePlay (Development)", helper.GameIdGooglePlayForDevelopment);
	            EditorGUI.indentLevel--;
            }
            
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Options", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            helper.InitializeOnStart = EditorGUILayout.Toggle("Initialize on Start", helper.InitializeOnStart);
            helper.EnableTestMode = EditorGUILayout.Toggle("Enable Test Mode", helper.EnableTestMode);
            EditorGUI.indentLevel--;
            
            EditorGUILayout.Space();
            
            //TODO: ここにリスナースクリプトを追加するボタン
        }
    }
}