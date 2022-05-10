using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace KID
{
    /// <summary>
    /// 管理器：選取地圖
    /// </summary>
    public class ManagerSelectionMap : MonoBehaviour
    {
        [SerializeField, Header("關卡按鈕")]
        private Button[] btnMaps;
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
            "關卡 1 竊盜案", "關卡 2", "關卡 3"
        };

        private void Awake()
        {
            rectSelect = GameObject.Find("選取圖示").GetComponent<RectTransform>();
            textMapSelect = GameObject.Find("場景標題").GetComponent<Text>();

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
                    SceneManager.LoadScene(nameMaps[index]);
                });
            }
        }
    }
}
