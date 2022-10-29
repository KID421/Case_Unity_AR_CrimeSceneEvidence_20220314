using UnityEngine;
using System.Collections;

namespace KID
{
    /// <summary>
    /// �H�H�X�e���s�դ���t��
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
        /// �}�l�H�J�e���s��
        /// </summary>
        /// <param name="_group">�e���s��</param>
        /// <param name="_fadeIn">�O�_�H�J</param>
        public void StartFadeCanvasGroup(CanvasGroup _group, bool _fadeIn = true)
        {
            group = _group;
            fadeIn = _fadeIn;

            StartCoroutine(FadeCanvasGroup());
        }

        /// <summary>
        /// �H�J�e���s��
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
