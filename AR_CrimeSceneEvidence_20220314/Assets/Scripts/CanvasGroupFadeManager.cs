using UnityEngine;
using System.Collections;

namespace KID
{
    /// <summary>
    /// �e���s�պ޲z���G�H�J�H�X
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
        /// �H�J�H�X
        /// </summary>
        /// <param name="_groupTarget">�s�եؼ�</param>
        /// <param name="_fadeIn">�O�_�H�J</param>
        public void Fade(CanvasGroup _groupTarget, bool _fadeIn = true)
        {
            groupTarget = _groupTarget;
            fadeIn = _fadeIn;
            StartCoroutine(FadeEffect());
        }

        /// <summary>
        /// �H�J�H�X�ĪG
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
