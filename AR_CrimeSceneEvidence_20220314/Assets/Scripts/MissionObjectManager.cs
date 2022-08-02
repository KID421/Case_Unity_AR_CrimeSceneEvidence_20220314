using UnityEngine;
using UnityEngine.UI;

namespace KID
{
    /// <summary>
    /// ���Ȫ���޲z��
    /// </summary>
    public class MissionObjectManager : MonoBehaviour
    {
        public static MissionObjectManager instance;

        [SerializeField, Header("�}�l�˴�")]
        private GameObject goBtnStartCheck;
        private Button btnStartCheck;

        /// <summary>
        /// ���ȶi��
        /// </summary>
        private Text textMission;
        private string stringMission = "�Ҫ��ƶq�G";
        private int countMissionFinish;

        private int countMissionTotal => DataLevelInteracte.instance.dataLevelGoal.Length;

        private void Start()
        {
            instance = this;

            btnStartCheck = goBtnStartCheck.GetComponent<Button>();

            textMission = GameObject.Find("���ȶi��").GetComponent<Text>();
            textMission.text = stringMission + "0 / " + countMissionTotal;
        }

        /// <summary>
        /// ��s����
        /// </summary>
        public void UpdateMission()
        {
            countMissionFinish++;

            textMission.text = stringMission + countMissionFinish + " / " + countMissionTotal;

            if (countMissionFinish == countMissionTotal) goBtnStartCheck.SetActive(true);
        }
    }
}

