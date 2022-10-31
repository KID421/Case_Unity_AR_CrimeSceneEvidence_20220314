using UnityEngine;
using UnityEngine.UI;

namespace KID
{
    /// <summary>
    /// �޲z�̫���Ƥ����A�٦��ɶ�
    /// </summary>
    public class ManagerFinalScore : MonoBehaviour
    {
        #region ����������
        /// <summary>
        /// ���s���ҵ���
        /// </summary>
        private Button btnCheckEnd;
        /// <summary>
        /// �e�����⤶��
        /// </summary>
        private CanvasGroup groupFinal;
        /// <summary>
        /// ��r�N��
        /// </summary>
        private Text textNumber;
        /// <summary>
        /// ��r��O�ɶ�
        /// </summary>
        private Text textTime;
        /// <summary>
        /// ��r�������
        /// </summary>
        private Text textScore;
        #endregion

        #region �ƥ�
        private void Awake()
        {
            FindObject();
            ButtonAddLinstener();
        }
        #endregion

        #region ��k
        /// <summary>
        /// �M�䪫��
        /// </summary>
        private void FindObject()
        {
            btnCheckEnd = GameObject.Find("���s���ҵ���").GetComponent<Button>();
            groupFinal = GameObject.Find("�e�����⤶��").GetComponent<CanvasGroup>();
            textNumber = GameObject.Find("��r�N��").GetComponent<Text>();
            textTime = GameObject.Find("��r��O�ɶ�").GetComponent<Text>();
            textScore = GameObject.Find("��r�������").GetComponent<Text>();
        }

        /// <summary>
        /// ���s�K�[��ť��
        /// </summary>
        private void ButtonAddLinstener()
        {
            btnCheckEnd.onClick.AddListener(() =>
            {
                UpdateTextTimeAndScore();
                ManagerTime.instance.isTimeStop = true;
                CanvasGroupFadeManager.instance.Fade(groupFinal);
            });
        }

        /// <summary>
        ///�޲z��r�G�ɶ��P����
        /// </summary>
        private void UpdateTextTimeAndScore()
        {
            textNumber.text = Random.Range(1, 1000).ToString("000000");

            string minute = ManagerTime.instance.minute.ToString("00");
            string second = ManagerTime.instance.second.ToString("00");
            textTime.text = string.Format("00 : {0} : {1}", minute, second);

            textScore.text = DataLevelInteracte.instance.scoreTotal + "";

            GoogleAppsScriptManager.instance.StartSetGAS();
        }
        #endregion
    }
}
