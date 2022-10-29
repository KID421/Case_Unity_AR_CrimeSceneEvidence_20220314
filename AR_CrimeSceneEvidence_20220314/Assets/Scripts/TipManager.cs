using UnityEngine;
using UnityEngine.UI;

namespace KID
{
    /// <summary>
    /// ���ܺ޲z��
    /// </summary>
    public class TipManager : MonoBehaviour
    {
        private CanvasGroup groupTip;
        private Button btnConfirmTip;

        private void Awake()
        {
            groupTip = GameObject.Find("�e������").GetComponent<CanvasGroup>();
            btnConfirmTip = GameObject.Find("���s�T������").GetComponent<Button>();
            btnConfirmTip.onClick.AddListener(() => FadeCanvasGroupSystem.instance.StartFadeCanvasGroup(groupTip, false));
        }
    }
}
