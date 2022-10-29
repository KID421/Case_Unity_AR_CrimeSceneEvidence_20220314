using UnityEngine;
using System.Collections;

namespace KID
{
    /// <summary>
    /// 淡淡出畫布群組元件系統
    /// </summary>
    public class FadeCanvasGroupSystem : MonoBehaviour
    {
        public static FadeCanvasGroupSystem instance;

        private CanvasGroup group;
        private bool fadeIn;

        private void Awake()
        {
            instance = this;
        }

        /// <summary>
        /// 開始淡入畫布群組
        /// </summary>
        /// <param name="_group">畫布群組</param>
        /// <param name="_fadeIn">是否淡入</param>
        public void StartFadeCanvasGroup(CanvasGroup _group, bool _fadeIn = true)
        {
            group = _group;
            fadeIn = _fadeIn;

            StartCoroutine(FadeCanvasGroup());
        }

        /// <summary>
        /// 淡入畫布群組
        /// </summary>
        private IEnumerator FadeCanvasGroup()
        {
            float increase = fadeIn ? +0.1f : -0.1f;

            for (int i = 0; i < 10; i++)
            {
                group.alpha += increase;
                yield return null;
            }

            group.interactable = fadeIn;
            group.blocksRaycasts = fadeIn;
        }
    }
}
