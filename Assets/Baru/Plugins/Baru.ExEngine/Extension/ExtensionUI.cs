using UnityEngine;
using UnityEngine.UI;


namespace Extension
{
    public static class ExtensionUI
    {
        // UI
        public static void AttachUI(this RectTransform uiWnd, RectTransform parent)
        {
            uiWnd.SetParent(parent);
            uiWnd.SetSiblingIndex(parent.childCount - 1);
        }

        public static void DetachUI(this RectTransform uiWnd, RectTransform parent = null)
        {
            uiWnd?.SetParent(parent);
        }

        public static int GetOpenUICount(this RectTransform uiWnd)
        {
            return uiWnd.childCount;
        }

        // Image
        public static void SetAlpha(this Image image, float alpha)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }

        // Text
        private static void ApplyText(Text txt, string str)
        {
            str = str.Replace("\\n", "\n");
            txt.text = str;
        }

        public static void SetText(this Text txt, string str)
        {
            if (null == txt)
            {
                Debug.Log("Extention SetText - Text is null.");
                return;
            }

            if (string.IsNullOrEmpty(str))
            {
                txt.text = str;
                return;
            }

            ApplyText(txt, str);
        }

        public static void SetText(this Text txt, string format, params object[] arg)
        {
            if(null == txt)
            {
                Debug.Log("Extention SetText - Text is null.");
                return;
            }

            if (string.IsNullOrEmpty(format))
            {
                txt.text = string.Empty;    
            }
            else
            {
                string str = string.Format(format, arg);
                ApplyText(txt, str);
            }
        }
    }

}
