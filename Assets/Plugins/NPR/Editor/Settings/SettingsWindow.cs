using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace NPR.Editor.Settings
{
    public class SettingsWindow : EditorWindow
    {
        // Fields

        private static SettingsWindow _window;

        private TAssetRepository[] _assets;
        private int _selectedIndex = 0;


        private const string MENU_ITEM_OPEN = "NPR/Settings";
        private const string MENU_TITLE = "NPR Settings";

        private const int MIN_WIDTH = 400;
        private const int MIN_HEIGHT = 600;


        // Methods

        [MenuItem(MENU_ITEM_OPEN)]
        public static void OpenWindow()
        {
            SetupWindow();
        }

        private static void SetupWindow()
        {
            if (_window != null) _window.Close();

            _window = GetWindow<SettingsWindow>();

            _window.minSize = new Vector2(MIN_WIDTH, MIN_HEIGHT);
        }

        private static int CompareAssetRepositories(TAssetRepository x, TAssetRepository y)
        {
            if (x == null || y == null) return 0;

            return x.Priority.CompareTo(y.Priority);
        }

        private void OnEnable()
        {
            titleContent = new GUIContent(MENU_TITLE);

            var repositoryGuids = AssetDatabase.FindAssets("t:TAssetRepository");

            _assets = new TAssetRepository[repositoryGuids.Length];

            for (int i = 0; i < repositoryGuids.Length; ++i)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(repositoryGuids[i]);

                _assets[i] = AssetDatabase.LoadAssetAtPath<TAssetRepository>(assetPath);
            }

            Array.Sort(_assets, CompareAssetRepositories);
        }

        private void OnGUI()
        {
            DrawHeader();

            if (_selectedIndex >= 0 && _selectedIndex < _assets.Length)
            {
                var selectedObject = _assets[_selectedIndex];

                if (selectedObject != null)
                {
                    var editor = UnityEditor.Editor.CreateEditor(selectedObject);

                    editor.OnInspectorGUI();
                }
            }
        }

        private void DrawHeader()
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            var names = _assets.Select(t => t.Name).ToArray();

            _selectedIndex = GUILayout.Toolbar(_selectedIndex, names, Styles.buttonStyle, GUI.ToolbarButtonSize.FitToContents);

            GUILayout.FlexibleSpace();

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }


        // Classes

        private static class Styles
        {
            public static readonly GUIStyle buttonStyle = "LargeButton";
        }
    }
}