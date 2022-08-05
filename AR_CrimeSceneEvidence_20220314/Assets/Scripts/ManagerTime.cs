using UnityEngine;
using UnityEngine.UI;

namespace KID
{
    /// <summary>
    /// �ɶ��޲z
    /// </summary>
    public class ManagerTime : MonoBehaviour
    {
        /// <summary>
        /// �O�_����
        /// </summary>
        public bool isTimeStop;

        private float timerCount;
        /// <summary>
        /// ��r�ɶ�
        /// </summary>
        private Text textTime;

        private float minute { get => Mathf.Floor(timerCount / 60); }
        private float second { get => Mathf.Floor(timerCount % 60); }

        public static ManagerTime instance;

        private void Awake()
        {
            instance = this;

            textTime = GameObject.Find("��r�ɶ�").GetComponent<Text>();
        }

        private void Update()
        {
            UpdateTimeAndUI();
        }

        /// <summary>
        /// ��s�ɶ��P����
        /// </summary>
        private void UpdateTimeAndUI()
        {
            if (isTimeStop) return;

            timerCount += Time.deltaTime;
            textTime.text = string.Format("00 : {0} : {1}", minute.ToString("00"), second.ToString("00"));
        }
    }
}
