using UnityEngine;
using UnityEngine.UI;

namespace KID
{
    /// <summary>
    /// 時間管理
    /// </summary>
    public class ManagerTime : MonoBehaviour
    {
        [SerializeField, Header("倒數時間單位分鐘"), Range(0, 30)]
        private float totalCountTime = 15;

        private float timerCount;
        private bool isTimeStop;
        /// <summary>
        /// 文字時間
        /// </summary>
        private Text textTime;

        private float minute { get => Mathf.Floor(timerCount / 60); }
        private float second { get => Mathf.Floor(timerCount % 60); }

        public delegate void delegateTimeStop();
        public delegateTimeStop onTimeStop;

        private void Awake()
        {
            textTime = GameObject.Find("文字時間").GetComponent<Text>();

            timerCount = totalCountTime * 60;
        }

        private void Update()
        {
            UpdateTimeAndUI();
        }

        /// <summary>
        /// 更新時間與介面
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

