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

        private Texture _texture;
        
        private void OnEnable()
        {
	        _texture = AssetDatabase.LoadAssetAtPath<Texture>("Assets/UnityAdsHelper/Editor/logo.png");
        }

        public override void OnInspectorGUI()
        {
	        var helper = target as UnityAdsHelper;
	        if (helper == null) return;

	        GUI.DrawTexture(new Rect(EditorGUIUtility.currentViewWidth/2-101.5f,10f, 203f, 50f), _texture);
	        
	        EditorGUILayout.Space(60f);
	        
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
            
            EditorGUILayout.LabelField("Unity Ads Listener", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            var onAdsReady = serializedObject.FindProperty("onAdsReady");
            EditorGUILayout.PropertyField(onAdsReady);
            var onAdsDidError = serializedObject.FindProperty("onAdsDidError");
            EditorGUILayout.PropertyField(onAdsDidError);
            var onAdsDidStart = serializedObject.FindProperty("onAdsDidStart");
            EditorGUILayout.PropertyField(onAdsDidStart);
            var onAdsFinished = serializedObject.FindProperty("onAdsFinished");
            EditorGUILayout.PropertyField(onAdsFinished);
            var onAdsSkipped = serializedObject.FindProperty("onAdsSkipped");
            EditorGUILayout.PropertyField(onAdsSkipped);
            var onAdsFailed = serializedObject.FindProperty("onAdsFailed");
            EditorGUILayout.PropertyField(onAdsFailed);
            EditorGUI.indentLevel--;
            
            // Apply modified properties
            serializedObject.ApplyModifiedProperties();
        }
    }
}