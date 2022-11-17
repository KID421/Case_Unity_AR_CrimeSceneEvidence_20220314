using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;

namespace KID
{
    [DefaultExecutionOrder(200)]
    /// <summary>
    /// Google App Script 管理器
    /// 儲存資料於 GAS 內
    /// </summary>
    public class GoogleAppsScriptManager : MonoBehaviour
    {
        private string gasURL = "https://script.google.com/macros/s/AKfycbzW2miVL3nlZHRC9L3lgVyYK8tX3ldsmyLJDrG-ssXwWE4czqQwpgOx1prxI8dOS2a2/exec";
        private string stringMethod = "method";
        private string stringSave = "儲存犯罪現場採證資料";
        private DataLevelInteracte dataLevel;

        public static GoogleAppsScriptManager instance;

        private void Awake()
        {
            instance = this;
            dataLevel = DataLevelInteracte.instance;
        }
        
        /// <summary>
        /// 開始設定 GAS 資料
        /// </summary>
        public void StartSetGAS()
        {
            StartCoroutine(StartSetSheetData());
        }

        /// <summary>
        /// 開始設定儲存資料
        /// </summary>
        private IEnumerator StartSetSheetData()
        {
            WWWForm form = new WWWForm();
            form.AddField(stringMethod, stringSave);
            form.AddField("scene", SceneManager.GetActiveScene().name);
            form.AddField("id", PlayerPrefs.GetString("id"));
            form.AddField("date", DateTime.Now.ToString());
            form.AddField("time", ManagerTime.instance.timerCount.ToString("F0"));
            form.AddField("score", DataLevelInteracte.instance.scoreTotal);

            for (int i = 0; i < dataLevel.dataLevelGoal.Length; i++)
            {
                string nameTitle = dataLevel.dataLevelGoal[i].name;
                string nameScore = dataLevel.dataLevelGoal[i].score.ToString();
                string nameUseDataPage = dataLevel.dataLevelGoal[i].useDataPage ? "有使用" : "沒使用";
            
                form.AddField("title" + i, nameTitle);
                form.AddField("score" + i, nameScore);
                form.AddField("use" + i, nameUseDataPage);
            }

            using (UnityWebRequest www = UnityWebRequest.Post(gasURL, form))
            {
                yield return www.SendWebRequest();

                print(www.downloadHandler.text);
            }
        }
    }
}
