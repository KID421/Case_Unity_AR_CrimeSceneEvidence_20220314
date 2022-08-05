using UnityEngine;
using UnityEngine.UI;

namespace KID
{
    /// <summary>
    /// 任務物件管理器
    /// </summary>
    public class MissionObjectManager : MonoBehaviour
    {
        public static MissionObjectManager instance;

        [SerializeField, Header("開始檢測")]
        private GameObject goBtnStartCheck;
        private Button btnStartCheck;

        /// <summary>
        /// 任務進度
        /// </summary>
        private Text textMission;
        private string stringMission = "證物數量：";
        private int countMissionFinish;
        /// <summary>
        /// 工具底圖
        /// </summary>
        private CanvasGroup groupTools;
        /// <summary>
        /// 畫布採證介面
        /// </summary>
        private CanvasGroup groupCheck;

        private int countMissionTotal => DataLevelInteracte.instance.dataLevelGoal.Length;

        private void Start()
        {
            instance = this;

            btnStartCheck = goBtnStartCheck.GetComponent<Button>();
            btnStartCheck.onClick.AddListener(StartCheck);

            textMission = GameObject.Find("任務進度").GetComponent<Text>();
            textMission.text = stringMission + "0 / " + countMissionTotal;

            groupTools = GameObject.Find("工具底圖").GetComponent<CanvasGroup>();
            groupCheck = GameObject.Find("畫布採證介面").GetComponent<CanvasGroup>();
        }

        /// <summary>
        /// 開始檢測
        /// </summary>
        private void StartCheck()
        {
            goBtnStartCheck.SetActive(false);
            groupTools.alpha = 0;
            groupTools.interactable = false;
            groupTools.blocksRaycasts = false;
            groupCheck.alpha = 1;
            groupCheck.interactable = true;
            groupCheck.blocksRaycasts = true;
        }

        /// <summary>
        /// 更新任務
        /// </summary>
        public void UpdateMission()
        {
            countMissionFinish++;

            textMission.text = stringMission + countMissionFinish + " / " + countMissionTotal;

            if (countMissionFinish == countMissionTotal) goBtnStartCheck.SetActive(true);
        }
    }
}

