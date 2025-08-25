using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

namespace NPR.Editor.Utilities
{
    public static class StyleSheetUtils
    {
        private static readonly string[] SHARED_STYLES =
        {
            "CommonElements.uss",
            "CommonValues.uss",
            "CommonColors.uss",
        };

        private static readonly Dictionary<int, StyleSheet> STYLESHEETS = new();

        public static void Load(ref VisualElement root, params string[] paths)
        {
            foreach (var style in SHARED_STYLES)
            {
                var path = PathUtils.COMMON_STYLES_DIRECTORY + style;

                root.styleSheets.Add(LoadStyleSheet(path));
            }

            foreach (var path in paths)
            {
                if (string.IsNullOrEmpty(path)) continue;

                root.styleSheets.Add(LoadStyleSheet(path));
            }
        }

        private static StyleSheet LoadStyleSheet(string path)
        {
            var completePath = path;

            if (STYLESHEETS.TryGetValue(completePath.GetHashCode(), out StyleSheet styleSheet))
            {
                return styleSheet;
            }

            styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(completePath);

            if (!styleSheet)
            {
                completePath = path;
                if (STYLESHEETS.TryGetValue(completePath.GetHashCode(), out styleSheet))
                {
                    return styleSheet;
                }

                styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(completePath);
            }

            STYLESHEETS.Add(completePath.GetHashCode(), styleSheet);

            return styleSheet;
        }
    }
}