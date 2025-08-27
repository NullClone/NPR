using UnityEditor;
using UnityEngine;

namespace Client.Editor.Creator
{
    public static class HLSLCreator
    {
        [MenuItem("Assets/Create/Shader/HLSL File", false, 100)]
        public static void CreateNewHLSLFile()
        {
            var icon = (Texture2D)EditorGUIUtility.IconContent("TextScriptImporter Icon").image;

            ProjectWindowUtil.CreateAssetWithContent("NewHLSL.hlsl", "", icon);
        }
    }
}