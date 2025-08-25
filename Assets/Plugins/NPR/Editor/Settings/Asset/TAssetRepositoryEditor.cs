using NPR.Editor.Utilities;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace NPR.Editor.Settings
{
    [CustomEditor(typeof(TAssetRepository), true)]
    public class TAssetRepositoryEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            StyleSheetUtils.Load(ref root, PathUtils.SETTINGS_STYLES_DIRECTORY + "Repository.uss");

            root.Add(new PropertyField(serializedObject.FindProperty("_repository")));

            return root;
        }
    }
}