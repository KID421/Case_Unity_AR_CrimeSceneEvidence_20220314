using UnityEngine;
using UnityEngine.UI;

namespace KID
{
    /// <summary>
    /// 提示管理器
    /// </summary>
    public class TipManager : MonoBehaviour
    {
        private CanvasGroup groupTip;
        private Button btnConfirmTip;

        private void Awake()
        {
            groupTip = GameObject.Find("畫布提示").GetComponent<CanvasGroup>();
            btnConfirmTip = GameObject.Find("按鈕確任提示").GetComponent<Button>();
            btnConfirmTip.onClick.AddListener(() => FadeCanvasGroupSystem.instance.StartFadeCanvasGroup(groupTip, false));
        }
    }
}
