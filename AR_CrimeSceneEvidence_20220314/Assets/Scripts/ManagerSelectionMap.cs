using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace KID
{
    /// <summary>
    /// 管理器：選取地圖
    /// </summary>
    public class ManagerSelectionMap : MonoBehaviour
    {
        [SerializeField, Header("關卡按鈕")]
        private Button[] btnMaps;
        [SerializeField, Header("關卡語音說明")]
        private AudioClip[] soundDescriptions;

        private RectTransform[] rectMaps;
        /// <summary>
        /// 選取圖示
        /// </summary>
        private RectTransform rectSelect;
        /// <summary>
        /// 場景標題
        /// </summary>
        private Text textMapSelect;
        private string[] nameMaps =
        {
            "住宅竊盜案", "兇殺事件", "特殊竊盜案"
        };
        private string[] descriptionMaps =
        {
            "李先生居住在小公寓，下班返家後，發現住家疑似遭人侵入，有財物損失，報警處理。",
            "王小姐居住在公寓小套房，有一日沒有來上班，同事也連絡不上，於是到宿舍找她，發現倒臥地上，已經沒有心跳...",
            "某大樓1樓發生一起住宅竊盜案，據被害屋主陳述共有金飾數條、珠寶一批等物遭竊，財物損失粗估達上百萬元..."
        };
        private AudioSource aud;
        private CanvasGroup groupLevelDescription;
        private Button btnSelectConfirm;
        private Button btnSelectCancel;
        private TextMeshProUGUI textDescription;

        private void Awake()
        {
            aud = GetComponent<AudioSource>();

            rectSelect = GameObject.Find("選取圖示").GetComponent<RectTransform>();
            textMapSelect = GameObject.Find("場景標題").GetComponent<Text>();

            groupLevelDescription = GameObject.Find("關卡介紹群組").GetComponent<CanvasGroup>();
            btnSelectConfirm = GameObject.Find("按鈕關卡確定").GetComponent<Button>();
            btnSelectCancel = GameObject.Find("按鈕關卡取消").GetComponent<Button>();
            textDescription = GameObject.Find("關卡介紹文字").GetComponent<TextMeshProUGUI>();

            btnSelectCancel.onClick.AddListener(() =>
            {
                btnSelectConfirm.onClick.RemoveAllListeners();
                aud.Stop();
                FadeCanvasGroupSystem.instance.StartFadeCanvasGroup(groupLevelDescription, false);
            });

            rectMaps = new RectTransform[btnMaps.Length];

            for (int i = 0; i < btnMaps.Length; i++)
            {
                rectMaps[i] = btnMaps[i].GetComponent<RectTransform>();
            }

            SelectMap();
        }

        /// <summary>
        /// 選取關卡
        /// </summary>
        private void SelectMap()
        {
            for (int i = 0; i < btnMaps.Length; i++)
            {
                int index = i;
                btnMaps[i].onClick.AddListener(() =>
                {
                    rectSelect.anchoredPosition = rectMaps[index].anchoredPosition + Vector2.up * 80;
                    textMapSelect.text = nameMaps[index];
                    FadeCanvasGroupSystem.instance.StartFadeCanvasGroup(groupLevelDescription);
                    aud.PlayOneShot(soundDescriptions[index]);
                    textDescription.text = descriptionMaps[index];

                    btnSelectConfirm.onClick.AddListener(() =>
                    {
                        btnSelectConfirm.interactable = false;
                        SceneManager.LoadScene(nameMaps[index]);
                    });
                });
            }
        }
    }
}
