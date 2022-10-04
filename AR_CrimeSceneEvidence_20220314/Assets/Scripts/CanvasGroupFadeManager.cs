using UnityEngine;
using System.Collections;

namespace KID
{
    /// <summary>
    /// 畫布群組管理器：淡入淡出
    /// </summary>
    public class CanvasGroupFadeManager : MonoBehaviour
    {
        public static CanvasGroupFadeManager instance;

        private CanvasGroup groupTarget;
        private bool fadeIn = true;
        private float fadePerTime = 0.1f;
        private float increase => fadeIn ? +fadePerTime : -fadePerTime;

        private void Awake()
        {
            instance = this;
        }

        /// <summary>
        /// 淡入淡出
        /// </summary>
        /// <param name="_groupTarget">群組目標</param>
        /// <param name="_fadeIn">是否淡入</param>
        public void Fade(CanvasGroup _groupTarget, bool _fadeIn = true)
        {
            groupTarget = _groupTarget;
            fadeIn = _fadeIn;
            StartCoroutine(FadeEffect());
        }

        /// <summary>
        /// 淡入淡出效果
        /// </summary>
        private IEnumerator FadeEffect()
        {
            for (int i = 0; i < 10; i++)
            {
                groupTarget.alpha += increase;
                yield return null;
            }

            groupTarget.interactable = fadeIn;
            groupTarget.blocksRaycasts = fadeIn;
        }
    }
}
