using UnityEngine;
using UnityEngine.UI;

namespace KID
{
    /// <summary>
    /// 管理最後分數介面，還有時間
    /// </summary>
    public class ManagerFinalScore : MonoBehaviour
    {
        #region 介面物件資料
        /// <summary>
        /// 按鈕採證結束
        /// </summary>
        private Button btnCheckEnd;
        /// <summary>
        /// 畫布結算介面
        /// </summary>
        private CanvasGroup groupFinal;
        /// <summary>
        /// 文字代號
        /// </summary>
        private Text textNumber;
        /// <summary>
        /// 文字花費時間
        /// </summary>
        private Text textTime;
        /// <summary>
        /// 文字測驗分數
        /// </summary>
        private Text textScore;
        #endregion

        #region 事件
        private void Awake()
        {
            FindObject();
            ButtonAddLinstener();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 尋找物件
        /// </summary>
        private void FindObject()
        {
            btnCheckEnd = GameObject.Find("按鈕採證結束").GetComponent<Button>();
            groupFinal = GameObject.Find("畫布結算介面").GetComponent<CanvasGroup>();
            textNumber = GameObject.Find("文字代號").GetComponent<Text>();
            textTime = GameObject.Find("文字花費時間").GetComponent<Text>();
            textScore = GameObject.Find("文字測驗分數").GetComponent<Text>();
        }

        /// <summary>
        /// 按鈕添加監聽器
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
        ///管理文字：時間與分數
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
