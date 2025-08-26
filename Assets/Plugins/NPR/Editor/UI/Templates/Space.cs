using UnityEngine.UIElements;

namespace NPR.Editor
{
    public class Space : VisualElement
    {
        public Space()
        {
            style.height = new StyleLength(5f);
        }

        public Space(float value)
        {
            style.height = new StyleLength(value);
        }
    }
}