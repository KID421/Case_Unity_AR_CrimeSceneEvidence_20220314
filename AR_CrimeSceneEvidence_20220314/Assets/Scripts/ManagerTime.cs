using UnityEngine;
using UnityEngine.UI;

namespace KID
{
    /// <summary>
    /// �ɶ��޲z
    /// </summary>
    public class ManagerTime : MonoBehaviour
    {
        [SerializeField, Header("�˼Ʈɶ�������"), Range(0, 30)]
        private float totalCountTime = 15;

        private float timerCount;
        private bool isTimeStop;
        /// <summary>
        /// ��r�ɶ�
        /// </summary>
        private Text textTime;

        private float minute { get => Mathf.Floor(timerCount / 60); }
        private float second { get => Mathf.Floor(timerCount % 60); }

        public delegate void delegateTimeStop();
        public delegateTimeStop onTimeStop;

        private void Awake()
        {
            textTime = GameObject.Find("��r�ɶ�").GetComponent<Text>();

            timerCount = totalCountTime * 60;
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

            timerCount -= Time.deltaTime;
            timerCount = Mathf.Clamp(timerCount, 0, totalCountTime * 60);
            textTime.text = string.Format("00 : {0} : {1}", minute.ToString("00"), second.ToString("00"));

            if (timerCount <= 0)
            {
                onTimeStop?.Invoke();
                isTimeStop = true;
            }
        }
    }
}

