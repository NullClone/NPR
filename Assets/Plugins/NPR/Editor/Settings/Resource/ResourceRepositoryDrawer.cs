using NPR.Editor.Utilities;
using UnityEditor;
using UnityEngine.UIElements;

namespace NPR.Editor.Settings
{
    [CustomPropertyDrawer(typeof(ResourceRepository))]
    public class ResourceRepositoryDrawer : PropertyDrawer
    {
        // Fields

        private const string NAME_CONTAINER_ROOT = "GC-Updates-Container-Root";
        private const string NAME_CONTAINER_BODY = "GC-Updates-Container-Body";

        private const string NAME_ASSET_ROOT = "GC-Updates-Asset-Root";
        private const string NAME_ASSET_HEAD = "GC-Updates-Asset-Head";
        private const string NAME_ASSET_BODY = "GC-Updates-Asset-Body";

        private VisualElement m_Root;
        private VisualElement m_Body;


        // Methods

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            m_Root = new VisualElement { name = NAME_CONTAINER_ROOT };
            m_Body = new VisualElement { name = NAME_CONTAINER_BODY };

            StyleSheetUtils.Load(ref m_Root, $"{PathUtils.SETTINGS_STYLES_DIRECTORY}/Updates.uss");

            foreach (var key in ResourceConfig.Games.Keys)
            {
                CreateAsset(key);
            }

            m_Root.Add(m_Body);

            return m_Root;
        }


        private void CreateAsset(string id)
        {
            var root = new VisualElement { name = NAME_ASSET_ROOT };
            var head = new VisualElement { name = NAME_ASSET_HEAD };
            var body = new VisualElement { name = NAME_ASSET_BODY };

            root.Add(head);
            root.Add(body);

            m_Body.Add(root);

            CreateHead(id, head, body);
            CreateBody(id, body);
        }

        private void CreateHead(string id, VisualElement head, VisualElement body)
        {
            var path = $"{PathUtils.RESOURCE_DIRECTORY}/{id}";

            var isInstalled = AssetDatabase.IsValidFolder(path);

            var expandButton = new Button
            {
                text = "=",
                style =
                {
                    width = new Length(20, LengthUnit.Pixel),
                    borderRightWidth = new StyleFloat(1)
                }
            };

            expandButton.clicked += () =>
            {
                body.style.display = (body.style.display == DisplayStyle.None) ? DisplayStyle.Flex : DisplayStyle.None;
                expandButton.text = (body.style.display == DisplayStyle.None) ? "=" : "-";
            };

            var installButton = new Button
            {
                text = isInstalled ? "UnInstall" : "Download",
                style =
                {
                    width = new Length(100, LengthUnit.Pixel),
                    borderLeftWidth = new StyleFloat(1)
                },
            };

            installButton.clicked += () =>
            {
                if (isInstalled)
                {
                    ResourceManager.DeleteResourcesAsync(id);
                }
                else
                {
                    ResourceManager.DownloadResourcesAsync(id);
                }
            };

            installButton.SetEnabled(true);

            head.Add(expandButton);
            head.Add(new LabelTitle(id));
            //head.Add(new Label("v 1.2"));
            //head.Add(new Image { image = icon });
            head.Add(installButton);
        }

        private void CreateBody(string id, VisualElement body)
        {
            body.Add(new LabelTitle($""));
            body.Add(new Space());
            body.Add(new Label($""));

            body.style.display = DisplayStyle.None;
        }
    }
}