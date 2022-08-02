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

        private int countMissionTotal => DataLevelInteracte.instance.dataLevelGoal.Length;

        private void Start()
        {
            instance = this;

            btnStartCheck = goBtnStartCheck.GetComponent<Button>();

            textMission = GameObject.Find("任務進度").GetComponent<Text>();
            textMission.text = stringMission + "0 / " + countMissionTotal;
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

