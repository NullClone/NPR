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
            return new PropertyField(serializedObject.FindProperty("_repository"));
        }
    }
}