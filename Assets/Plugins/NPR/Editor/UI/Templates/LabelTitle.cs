using UnityEngine;
using UnityEngine.UIElements;

namespace NPR.Editor
{
    public sealed class LabelTitle : Label
    {
        public LabelTitle() : base()
        {
            style.unityFontStyleAndWeight = FontStyle.Bold;
            style.marginTop = new StyleLength(3);
            style.marginBottom = new StyleLength(3);
        }

        public LabelTitle(string value) : this()
        {
            text = value;
        }
    }
}