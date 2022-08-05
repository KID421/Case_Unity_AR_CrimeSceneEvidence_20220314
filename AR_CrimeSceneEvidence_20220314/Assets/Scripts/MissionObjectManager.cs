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
        /// <summary>
        /// �u�㩳��
        /// </summary>
        private CanvasGroup groupTools;
        /// <summary>
        /// �e�����Ҥ���
        /// </summary>
        private CanvasGroup groupCheck;

        private int countMissionTotal => DataLevelInteracte.instance.dataLevelGoal.Length;

        private void Start()
        {
            instance = this;

            btnStartCheck = goBtnStartCheck.GetComponent<Button>();
            btnStartCheck.onClick.AddListener(StartCheck);

            textMission = GameObject.Find("���ȶi��").GetComponent<Text>();
            textMission.text = stringMission + "0 / " + countMissionTotal;

            groupTools = GameObject.Find("�u�㩳��").GetComponent<CanvasGroup>();
            groupCheck = GameObject.Find("�e�����Ҥ���").GetComponent<CanvasGroup>();
        }

        /// <summary>
        /// �}�l�˴�
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

