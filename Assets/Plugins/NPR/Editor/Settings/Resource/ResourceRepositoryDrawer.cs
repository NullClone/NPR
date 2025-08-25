using NPR.Editor.Utilities;
using UnityEditor;
using UnityEngine.UIElements;

namespace NPR.Editor.Settings
{
    [CustomPropertyDrawer(typeof(ResourceRepository))]
    public class ResourceRepositoryDrawer : PropertyDrawer
    {
        // Fields

        private const string USS_PATH = PathUtils.SETTINGS_STYLES_DIRECTORY + "Updates.uss";

        private const string EXPAND_MORE = "+";
        private const string EXPAND_LESS = "-";

        private const string NAME_CONTAINER_ROOT = "GC-Updates-Container-Root";
        private const string NAME_CONTAINER_BODY = "GC-Updates-Container-Body";
        private const string NAME_CONTAINER_FOOT = "GC-Updates-Container-Foot";

        private const string NAME_ASSET_ROOT = "GC-Updates-Asset-Root";
        private const string NAME_ASSET_HEAD = "GC-Updates-Asset-Head";
        private const string NAME_ASSET_BODY = "GC-Updates-Asset-Body";

        private VisualElement m_Root;
        private VisualElement m_Body;
        private VisualElement m_Foot;


        // Methods

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            m_Root = new VisualElement { name = NAME_CONTAINER_ROOT };
            m_Body = new VisualElement { name = NAME_CONTAINER_BODY };
            m_Foot = new VisualElement { name = NAME_CONTAINER_FOOT };

            StyleSheetUtils.Load(ref m_Root, USS_PATH);

            RefreshAsset();

            m_Root.Add(m_Body);
            m_Root.Add(m_Foot);

            return m_Root;
        }

        private void RefreshAsset()
        {
            var root = new VisualElement { name = NAME_ASSET_ROOT };
            var head = new VisualElement { name = NAME_ASSET_HEAD };
            var body = new VisualElement { name = NAME_ASSET_BODY };

            root.Add(head);
            root.Add(body);

            m_Body.Add(root);

            CreateHead(head, body);
            CreateBody(body);
        }

        private void CreateHead(VisualElement head, VisualElement body)
        {
            /*
            Texture icon = isInstalled
                ? isInstalledOlder ? ICON_INSTALLED_UPD.Texture : ICON_INSTALLED_YES.Texture
                : ICON_INSTALLED_NO.Texture;
            */

            var btnExpand = new Button
            {
                text = EXPAND_MORE,
                style =
                {
                    width = new Length(20, LengthUnit.Pixel),
                    borderRightWidth = new StyleFloat(1)
                }
            };

            btnExpand.clicked += () =>
            {
                body.style.display = body.style.display == DisplayStyle.None
                    ? DisplayStyle.Flex
                    : DisplayStyle.None;

                btnExpand.text = body.style.display == DisplayStyle.None
                    ? EXPAND_MORE
                    : EXPAND_LESS;
            };

            var btnInstall = new Button
            {
                /*
                text = isInstalled
                    ? isInstalledOlder ? "Update" : "Installed"
                    : "Download",
                */
                text = "Update",
                style =
                {
                    width = new Length(100, LengthUnit.Pixel),
                    borderLeftWidth = new StyleFloat(1)
                }
            };

            /*
            btnInstall.clicked += () =>
            {
                Application.OpenURL(string.Format(STORE_LINK, id));
            };
            */

            btnInstall.SetEnabled(true);

            head.Add(btnExpand);
            head.Add(new LabelTitle("TextUtils.Humanize(id)"));
            head.Add(new Label("label"));
            //head.Add(new Image { image = icon });
            head.Add(btnInstall);
        }

        private void CreateBody(VisualElement body)
        {
            body.Add(new LabelTitle($""));
            body.Add(new Space());
            body.Add(new Label($""));

            body.style.display = DisplayStyle.None;
        }
    }
}