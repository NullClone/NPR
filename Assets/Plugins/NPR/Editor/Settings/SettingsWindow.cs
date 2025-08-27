using System;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace NPR.Editor.Settings
{
    public class SettingsWindow : EditorWindow
    {
        // Fields

        public static SettingsWindow _window;

        public int _selectedIndex = 0;
        private InspectorElement _inspectorElement;
        private TAssetRepository[] _assets;


        private const string MENU_ITEM_OPEN = "NPR/Settings";
        private const string MENU_TITLE = "NPR Settings";

        private const int MIN_WIDTH = 400;
        private const int MIN_HEIGHT = 600;


        // Methods

        [MenuItem(MENU_ITEM_OPEN)]
        public static void OpenWindow()
        {
            if (_window != null) _window.Close();

            _window = GetWindow<SettingsWindow>();

            _window.minSize = new Vector2(MIN_WIDTH, MIN_HEIGHT);
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


            static int CompareAssetRepositories(TAssetRepository x, TAssetRepository y)
            {
                if (x == null || y == null) return 0;

                return x.Priority.CompareTo(y.Priority);
            }
        }

        private void CreateGUI()
        {
            var inspectorContainer = new VisualElement();

            if (_inspectorElement == null)
            {
                OnChange(inspectorContainer);
            }

            var imguiContainer = new IMGUIContainer
            {
                onGUIHandler = () =>
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();

                    GUILayout.FlexibleSpace();

                    EditorGUI.BeginChangeCheck();

                    _selectedIndex = GUILayout.Toolbar(
                        _selectedIndex,
                        _assets.Select(t => t.Name).ToArray(),
                        Styles.buttonStyle,
                        GUI.ToolbarButtonSize.FitToContents);

                    if (EditorGUI.EndChangeCheck())
                    {
                        OnChange(inspectorContainer);
                    }

                    GUILayout.FlexibleSpace();

                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();
                }
            };

            rootVisualElement.Add(imguiContainer);
            rootVisualElement.Add(inspectorContainer);
        }

        private void OnChange(VisualElement visualElement)
        {
            var asset = _assets[_selectedIndex];

            if (asset != null)
            {
                if (_inspectorElement != null)
                {
                    visualElement.Clear();
                }

                _inspectorElement = new InspectorElement(asset);

                visualElement.Add(_inspectorElement);
            }
        }


        // Classes

        private static class Styles
        {
            public static readonly GUIStyle buttonStyle = "LargeButton";
        }
    }
}