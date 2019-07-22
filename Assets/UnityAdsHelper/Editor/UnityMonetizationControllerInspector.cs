using UnityEditor;
using UnityEngine;

namespace UnityAdsHelper.Editor
{
    [CustomEditor(typeof(UnityMonetizationController))]
    public class UnityMonetizationControllerInspector : UnityEditor.Editor
    {
        private const string OperateDashboardUrl = "https://operate.dashboard.unity3d.com";
        private const string UnityAdsKnowledgebaseUrl = "https://unityads.unity3d.com/help/index";
        private const string UnityAdsForumUrl = "https://forum.unity.com/forums/unity-ads.67/";
        
        private UnityMonetizationController _unityMonetizationController;
        private int _currentPickerControlId;

        private void OnEnable()
        {
            _unityMonetizationController = _unityMonetizationController ? _unityMonetizationController : (UnityMonetizationController) target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Dashboard")) Help.BrowseURL(OperateDashboardUrl);
            if (GUILayout.Button("Docs")) Help.BrowseURL(UnityAdsKnowledgebaseUrl);
            if (GUILayout.Button("Forum")) Help.BrowseURL(UnityAdsForumUrl);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.Space();
            
            _unityMonetizationController.unityAdsSettings = EditorGUILayout.ObjectField("Unity Ads Settings", 
                _unityMonetizationController.unityAdsSettings, typeof(UnityAdsSettings), false) as UnityAdsSettings;
            if (_unityMonetizationController.unityAdsSettings == null)
            {
                var unityAdsSettingsGuid = AssetDatabase.FindAssets("t:UnityAdsSettings");
                if (unityAdsSettingsGuid.Length == 0)
                {
                    ShowCreateAdsSettingsButton();
                }
                else
                {
                    EditorGUILayout.BeginHorizontal();
                    ShowCreateAdsSettingsButton();
                    _currentPickerControlId = GUIUtility.GetControlID(FocusType.Passive);
                    using (new BackgroundColorScope(Color.yellow))
                    {
                        if (GUILayout.Button("Choose Settings File"))
                        {
                            EditorGUIUtility.ShowObjectPicker<UnityAdsSettings>(
                                _unityMonetizationController.unityAdsSettings,
                                false, "t:UnityAdsSettings", _currentPickerControlId);
//                            var path = AssetDatabase.GUIDToAssetPath(unityAdsSettingsGuid[0]);
//                            _unityMonetizationController.UnityAdsSettings =
//                                AssetDatabase.LoadAssetAtPath<UnityAdsSettings>(path);
                        }

                        if (Event.current.commandName == "ObjectSelectorUpdated" &&
                            EditorGUIUtility.GetObjectPickerControlID() == _currentPickerControlId)
                        {
                            _unityMonetizationController.unityAdsSettings = (UnityAdsSettings) EditorGUIUtility.GetObjectPickerObject();
//                            EditorUtility.SetDirty(_unityMonetizationController);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            else
            {
                using (new BackgroundColorScope(Color.green))
                {
                    if (GUILayout.Button("See Unity Ads Settings"))
                    {
                        Selection.SetActiveObjectWithContext(_unityMonetizationController.unityAdsSettings,
                            _unityMonetizationController);
                    }
                }
            }

            _unityMonetizationController.unityAdsListener = EditorGUILayout.ObjectField("Unity Ads Listener",
                _unityMonetizationController.unityAdsListener, typeof(UnityAdsListener), false) as UnityAdsListener;
            if (_unityMonetizationController.unityAdsListener == null)
            {
                var unityAdsListenerGuid = AssetDatabase.FindAssets("t:UnityAdsListener");
                if (unityAdsListenerGuid.Length == 0)
                {
                    ShowCreateAdsListenerButton();
                }
                else
                {
                    EditorGUILayout.BeginHorizontal();
                    ShowCreateAdsListenerButton();
                    _currentPickerControlId = GUIUtility.GetControlID(FocusType.Passive);
                    using (new BackgroundColorScope(Color.yellow))
                    {
                        if (GUILayout.Button("Choose Listener File"))
                        {
                            EditorGUIUtility.ShowObjectPicker<UnityAdsListener>(
                                _unityMonetizationController.unityAdsListener,
                                false, "t:UnityAdsListener", _currentPickerControlId);
//                            var path = AssetDatabase.GUIDToAssetPath(unityAdsListenerGuid[0]);
//                            _unityMonetizationController.UnityAdsListener =
//                                AssetDatabase.LoadAssetAtPath<UnityAdsListener>(path);
                        }
                        if (Event.current.commandName == "ObjectSelectorUpdated" &&
                            EditorGUIUtility.GetObjectPickerControlID() == _currentPickerControlId)
                        {
                            _unityMonetizationController.unityAdsListener = (UnityAdsListener) EditorGUIUtility.GetObjectPickerObject();
//                            EditorUtility.SetDirty(_unityMonetizationController);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            else
            {
                using (new BackgroundColorScope(Color.green))
                {
                    if (GUILayout.Button("See Unity Ads Listener"))
                    {
                        Selection.SetActiveObjectWithContext(_unityMonetizationController.unityAdsListener,
                            _unityMonetizationController);
                    }
                }
            }
//            EditorUtility.SetDirty(_unityMonetizationController);
        }

        private void ShowCreateAdsSettingsButton()
        {
            using (new BackgroundColorScope(Color.red))
            {
                if (GUILayout.Button("Create Unity Ads Settings File"))
                {
                    EditorApplication.ExecuteMenuItem("Assets/Create/UnityAdsHelper/Create Settings File");
                }
            }
        }

        private void ShowCreateAdsListenerButton()
        {
            using (new BackgroundColorScope(Color.red))
            {
                if (GUILayout.Button("Create Unity Ads Listener File"))
                {
                    EditorApplication.ExecuteMenuItem("Assets/Create/UnityAdsHelper/Create Listener File");
                }
            }
        }
    }
    
    public class BackgroundColorScope : GUI.Scope
    {
        private readonly Color _color;
        public BackgroundColorScope(Color color)
        {
            this._color = GUI.backgroundColor;
            GUI.backgroundColor = color;
        }
		
        protected override void CloseScope()
        {
            GUI.backgroundColor = _color;
        }
    }
}